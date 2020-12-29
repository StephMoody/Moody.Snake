using System;
using System.Linq;
using System.Reflection;
using System.Windows;
using Autofac;
using Autofac.Builder;
using Autofac.Core;
using Autofac.Core.Resolving.Pipeline;
using Moody.MVVM.Base.ViewModel;

namespace Moody.UI.Autofac
{
    public static class AutofacExtensions
    {
        private const string ViewModel = "ViewModel";

        public static void ForViewModel<TLimit, TActivatorData, TRegistrationStyle>(this IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle> registrationBuilder, Type viewModelType)
        {
            registrationBuilder.WithMetadata(ViewModel, viewModelType);
        }

        public static void WithViewModelParameter <TLimit, TReflectionActivatorData, TStyle>(
        this IRegistrationBuilder<TLimit, TReflectionActivatorData, TStyle> registrationBuilder) where TReflectionActivatorData : ReflectionActivatorData
        {
            registrationBuilder.WithParameter(new ResolvedParameter(Predicate, ValueAccessor));
        }
        private static object ValueAccessor(ParameterInfo parameterInfo, IComponentContext context)
        {
            if (!TryGetAssociatedViewModelType(parameterInfo, context, out Type type)) 
                return null;

            return context.Resolve(type);
        }
        
        private static bool Predicate(ParameterInfo parameterInfo, IComponentContext context)
        {
            return TryGetAssociatedViewModelType(parameterInfo, context, out _);
        }

        private static bool TryGetAssociatedViewModelType(ParameterInfo parameterInfo, IComponentContext context,
            out Type type)
        {
            type = default;
            if (parameterInfo.ParameterType != typeof(ViewModelBase))
                return false;

            if (!(context is ResolveRequestContext resolveRequestContext))
                return false;

            if (!(resolveRequestContext.Service is TypedService typedService))
                return false;

            Type window = typedService.ServiceType.GenericTypeArguments.FirstOrDefault(b => b.BaseType == typeof(Window));

            if (window == null)
                return false;

            if (!context.ComponentRegistry.TryGetRegistration(new TypedService(window),
                out IComponentRegistration registration))
                return false;

            if (!registration.Metadata.TryGetValue(ViewModel, out object metaDataValue))
                return false;

            if (!(metaDataValue is Type metaDataValueType))
                return false;
            
            type = metaDataValueType;
            return true;
        }
    }
}