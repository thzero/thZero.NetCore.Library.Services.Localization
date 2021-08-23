/* ------------------------------------------------------------------------- *
thZero.NetCore.Library.Asp
Copyright (C) 2016-2021 thZero.com

<development [at] thzero [dot] com>

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

	http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
 * ------------------------------------------------------------------------- */

using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Threading;

namespace thZero.Services
{
    /// <summary>
    /// Globalization and Localization using https://docs.asp.net/en/latest/fundamentals/localization.html
    /// </summary>
    public sealed class ServiceLocalizationFactory : Internal.ServiceLocalizationBase<ServiceLocalizationFactory>, IServiceLocalization
    {
        private static readonly thZero.Services.IServiceLog log = thZero.Factory.Instance.RetrieveLogger(typeof(ServiceLocalizationFactory));

        public ServiceLocalizationFactory() : base(log, null)
        {
        }
    }

    public sealed class ServiceLocalization : ServiceBase<ServiceLocalization>, IServiceLocalization
    {
        public ServiceLocalization(ILogger<ServiceLocalization> logger) : base(logger)
        {
            _instance = new Internal.ServiceLocalizationBase<ServiceLocalization>(null, logger);
        }

        #region Public Methods
        public void Initialize(IServiceLocalizationIntializer initializer, Type type)
        {
            _instance.Initialize(initializer, type);
        }

        #region Add Cultures
        public void AddCultureResource(string resourceName)
        {
            _instance.AddCultureResource(resourceName);
        }

        public void AddCultureResource(System.Reflection.Assembly assembly)
        {
            _instance.AddCultureResource(assembly);
        }

        public void AddCultureResourceType(CultureInfo culture, string name)
        {
            _instance.AddCultureResourceType(culture, name);
        }
        #endregion

        #region Get Localized Strings
        public string GetLocalizedString(string abbreviation, params object[] args)
        {
            return _instance.GetLocalizedString(abbreviation, args);
        }

        public string GetLocalizedStringDefault(string abbreviation, string defaultValue, params object[] args)
        {
            return _instance.GetLocalizedString(abbreviation, defaultValue, args);
        }

        public string GetLocalizedStringWithResource(string abbreviation, string resource, params object[] args)
        {
            return _instance.GetLocalizedString(abbreviation, resource, args);
        }

        public string GetLocalizedStringWithResourceDefault(string abbreviation, string resource, string defaultValue, params object[] args)
        {
            return _instance.GetLocalizedString(abbreviation, resource, defaultValue, args);
        }

        public string GetLocalizedString(CultureInfo culture, string abbreviation, params object[] args)
        {
            return _instance.GetLocalizedString(culture, abbreviation, args);
        }

        public string GetLocalizedStringDefault(CultureInfo culture, string abbreviation, string defaultValue, params object[] args)
        {
            return _instance.GetLocalizedString(culture, abbreviation, defaultValue, args);
        }

        public string GetLocalizedStringWithResource(CultureInfo culture, string abbreviation, string resource, params object[] args)
        {
            return _instance.GetLocalizedString(culture, abbreviation, resource, args);
        }

        public string GetLocalizedStringWithResourceDefault(CultureInfo culture, string abbreviation, string resource, string defaultValue, params object[] args)
        {
            return _instance.GetLocalizedString(culture, abbreviation, resource, defaultValue, args);
        }
        #endregion

        #region Load
        public void LoadCultureResources(string rootPath)
        {
            _instance.LoadCultureResources(rootPath);
        }

        public void LoadCultureResources(string rootPath, string resourceFolder)
        {
            _instance.LoadCultureResources(rootPath, resourceFolder);
        }

        public void LoadCultureResourcesClient()
        {
            _instance.LoadCultureResourcesClient();
        }

        public void LoadCultureResourcesAll(string rootPath, string resourceFolder)
        {
            _instance.LoadCultureResourcesAll(rootPath, resourceFolder);
        }

        public void LoadCultureResourcesAll(string rootPath, string resourceFolder, CultureInfo defaultCulture)
        {
            _instance.LoadCultureResourcesAll(rootPath, resourceFolder, defaultCulture);
        }

        public void LoadCultureResources(CultureInfo culture, string rootPath, string resourceFolder)
        {
            _instance.LoadCultureResources(culture, rootPath, resourceFolder);
        }

        public void LoadCultureResources(CultureInfo culture, string rootPath, string resourceFolder, CultureInfo defaultCulture)
        {
            _instance.LoadCultureResources(culture, rootPath, resourceFolder, defaultCulture);
        }
        #endregion

        #endregion

        #region Public Properties
        public CultureInfo DefaultCulture
        {
            get { return _instance.DefaultCulture; }
            set { _instance.DefaultCulture = value; }
        }

        public string DefaultResource
        {
            get { return _instance.DefaultResource; }
            set { _instance.DefaultResource = value; }
        }

        public string ResourceFolder
        {
            get { return _instance.ResourceFolder; }
            set { _instance.ResourceFolder = value; }
        }

        public string RootPath
        {
            get { return _instance.RootPath; }
            set { _instance.RootPath = value; }
        }
        #endregion

        #region Fields
        private static Internal.ServiceLocalizationBase<ServiceLocalization> _instance;
        #endregion
    }

    public class ServiceLocalizationInitializer : IServiceLocalizationIntializer
    {
        #region Public Properties
        public IStringLocalizerFactory Factory { get; set; }
        #endregion
    }
}

namespace thZero.Services.Internal
{
    /// <summary>
    /// Globalization and Localization using https://docs.asp.net/en/latest/fundamentals/localization.html
    /// </summary>
    public class ServiceLocalizationBase<TService> : IntermediaryServiceBase<TService>
    {
        public ServiceLocalizationBase(thZero.Services.IServiceLog log, ILogger<TService> logger) : base(log, logger)
        {
        }

        #region Public Methods
        public void Initialize(IServiceLocalizationIntializer initializer, Type type)
        {
            ResourceType = null;
            ResourceType = type;

            _initializer = initializer as ServiceLocalizationInitializer;
            if (_initializer == null)
                throw new InvalidInitializeLocationException();

            _factory = _initializer.Factory;
            if (_factory == null)
                throw new InvalidFactoryLocationException();
        }

        #region Add Cultures
        public void AddCultureResource(string resourceName)
        {
        }

        public void AddCultureResource(System.Reflection.Assembly assembly)
        {
        }

        public void AddCultureResourceType(CultureInfo culture, string name)
        {
        }
        #endregion

        #region Get Localized Strings
        public string GetLocalizedString(string abbreviation, params object[] args)
        {
            return GetResourceValue(CultureInfo.CurrentUICulture, abbreviation, null, null, args);
        }

        public string GetLocalizedStringDefault(string abbreviation, string defaultValue, params object[] args)
        {
            return GetResourceValue(CultureInfo.CurrentUICulture, abbreviation, null, defaultValue, args);
        }

        public string GetLocalizedStringWithResource(string abbreviation, string resource, params object[] args)
        {
            return GetResourceValue(CultureInfo.CurrentUICulture, abbreviation, resource, null, args);
        }

        public string GetLocalizedStringWithResourceDefault(string abbreviation, string resource, string defaultValue, params object[] args)
        {
            return GetResourceValue(CultureInfo.CurrentUICulture, abbreviation, resource, defaultValue, args);
        }

        public string GetLocalizedString(CultureInfo culture, string abbreviation, params object[] args)
        {
            return GetResourceValue(culture, abbreviation, null, null, args);
        }

        public string GetLocalizedStringDefault(CultureInfo culture, string abbreviation, string defaultValue, params object[] args)
        {
            return GetResourceValue(culture, abbreviation, null, defaultValue, args);
        }

        public string GetLocalizedStringWithResource(CultureInfo culture, string abbreviation, string resource, params object[] args)
        {
            return GetResourceValue(culture, abbreviation, resource, null, args);
        }

        public string GetLocalizedStringWithResourceDefault(CultureInfo culture, string abbreviation, string resource, string defaultValue, params object[] args)
        {
            return GetResourceValue(culture, abbreviation, resource, defaultValue, args);
        }
        #endregion

        #region Load
        public void LoadCultureResources(string rootPath)
        {
        }

        public void LoadCultureResources(string rootPath, string resourceFolder)
        {
        }

        public void LoadCultureResourcesClient()
        {
        }

        public void LoadCultureResourcesAll(string rootPath, string resourceFolder)
        {
        }

        public void LoadCultureResourcesAll(string rootPath, string resourceFolder, CultureInfo defaultCulture)
        {
        }

        public void LoadCultureResources(CultureInfo culture, string rootPath, string resourceFolder)
        {
        }

        public void LoadCultureResources(CultureInfo culture, string rootPath, string resourceFolder, CultureInfo defaultCulture)
        {
        }
        #endregion

        #endregion

        #region Public Properties
        public CultureInfo DefaultCulture
        {
            get { return CultureInfo.CurrentUICulture; }
            set { }
        }

        public string DefaultResource
        {
            get { return _defaultResource; }
            set { _defaultResource = value; }
        }

        public string ResourceFolder
        {
            get;
            set;
        }

        public string RootPath
        {
            get;
            set;
        }
        #endregion

        #region Private Methods
        private static IStringLocalizer GetLocalizer(string key)
        {
            if (_localizers.ContainsKey(key))
                return _localizers[key];

            lock (_lockLocalizer)
            {
                if (_localizers.ContainsKey(key))
                    return _localizers[key];

                var assemblyName = ResourceType.GetTypeInfo().Assembly.GetName().Name;
                var localizer = _factory.Create(key, assemblyName);
                _localizers.Add(key, localizer);
                return localizer;
            }
        }

        private string GetResourceValue(CultureInfo culture, string expression, string resource, string defaultValue, object[] args)
        {
            Enforce.AgainstNull(() => culture);
            Enforce.AgainstNullOrEmpty(() => expression);

            const string Declaration = "GetResourceValue";

            try
            {
                LockCache.EnterUpgradeableReadLock();

                if (expression.Contains(","))
                {
                    string[] split = expression.Split(',');
                    resource = split[0].Trim();
                }

                if (string.IsNullOrEmpty(resource))
                    resource = DefaultResource;

                string value = GetResourceValueFromCache(expression, resource, culture);
                if (value != null)
                {
                    if ((args != null) && (args.Length > 0))
                        value = string.Format(value, args);

                    return value;
                }

                try
                {
                    LockCache.EnterWriteLock();

                    value = string.Concat("[", expression, "]");
                    if (defaultValue != null)
                        value = defaultValue;

                    bool canCache = true;
                    IStringLocalizer localizer = GetLocalizer(resource);
                    if (localizer == null)
                    {
                        canCache = false;
                        if (defaultValue != null)
                            value = defaultValue;
                    }

                    var output = localizer[expression];
                    if (output.ResourceNotFound)
                    {
                        canCache = false;
                        if (defaultValue != null)
                            value = defaultValue;
                    }
                    else
                        value = output.Value;

                    if (canCache)
                    {
                        Log?.Diagnostic(Declaration, "Expression", expression);
                        Logger?.LogDebug2(Declaration, "Expression", expression);
                        SetResourceValueToCache(value, expression, resource, culture);
                    }
                }
                finally
                {
                    LockCache.ExitWriteLock();
                }

                if ((args != null) && (args.Length > 0))
                    value = string.Format(value, args);

                return value;
            }
            catch (Exception ex)
            {
                Log?.Error(Declaration, ex);
                Logger?.LogError2(Declaration, ex);
                throw;
            }
            finally
            {
                LockCache.ExitUpgradeableReadLock();
            }
        }

        private string GetResourceValueFromCache(string expression, string resource, CultureInfo culture)
        {
            Enforce.AgainstNull(() => culture);
            Enforce.AgainstNullOrEmpty(() => expression);
            Enforce.AgainstNullOrEmpty(() => resource);

            const string Declaration = "GetResourceValueFromCache";

            try
            {
                if (!_cache.ContainsKey(resource))
                    return null;

                CacheInfoResource resourceContent = _cache[resource];
                if (!resourceContent.Cultures.ContainsKey(culture.Name))
                    return null;

                CacheInfoCulture cultures = resourceContent.Cultures[culture.Name];
                if (!cultures.Values.ContainsKey(expression))
                    return null;

                return cultures.Values[expression];
            }
            catch (Exception ex)
            {
                Log?.Error(Declaration, ex);
                Logger?.LogError2(Declaration, ex);
                throw;
            }
        }

        private void SetResourceValueToCache(string value, string expression, string resource, CultureInfo culture)
        {
            Enforce.AgainstNull(() => culture);
            Enforce.AgainstNullOrEmpty(() => expression);

            const string Declaration = "SetResourceValueToCache";

            try
            {
                if (string.IsNullOrEmpty(value))
                    return;

                if (!expression.Contains(","))
                {
                    if (string.IsNullOrEmpty(resource))
                        resource = DefaultResource;
                }
                else
                {
                    string[] values = expression.Split(',');
                    resource = values[0].Trim();
                }

                CacheInfoResource contentResource = new();
                if (!_cache.ContainsKey(resource))
                    _cache.Add(resource, contentResource);
                else
                    contentResource = _cache[resource];

                CacheInfoCulture contentCulture = new();
                if (!contentResource.Cultures.ContainsKey(culture.Name))
                    contentResource.Cultures.Add(culture.Name, contentCulture);
                else
                    contentCulture = contentResource.Cultures[culture.Name];

                contentCulture.Values.Add(expression, value);
            }
            catch (Exception ex)
            {
                Log?.Error(Declaration, ex);
                Logger?.LogError2(Declaration, ex);
                throw;
            }
        }
        #endregion

        #region Private Properties
        private static Type ResourceType { get; set; }
        #endregion

        #region Fields
        private static readonly Dictionary<string, CacheInfoResource> _cache = new();
        private static string _defaultResource = "strings";
        private static IStringLocalizerFactory _factory;
        private static ServiceLocalizationInitializer _initializer;
        private static readonly IDictionary<string, IStringLocalizer> _localizers = new Dictionary<string, IStringLocalizer>();
        private static readonly object _lockLocalizer = new();

        private static readonly ReaderWriterLockSlim LockCache = new();
        #endregion
    }

    public class InvalidInitializeLocationException : Exception
    {
        public InvalidInitializeLocationException() : base("Invalid initializer.")
        {
        }
    }

    public class InvalidFactoryLocationException : Exception
    {
        public InvalidFactoryLocationException() : base("Invalid factory.")
        {
        }
    }

    public class CacheInfoResource
    {
        #region Public Operators
        public static bool operator ==(CacheInfoResource c1, CacheInfoResource c2)
        {
            Enforce.AgainstNull(() => c1);
            Enforce.AgainstNull(() => c2);

            return c1.Equals(c2);
        }

        public static bool operator !=(CacheInfoResource c1, CacheInfoResource c2)
        {
            Enforce.AgainstNull(() => c1);
            Enforce.AgainstNull(() => c2);

            return !c1.Equals(c2);
        }
        #endregion

        #region Public Methods
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (!(obj is CacheInfoResource))
                return false;

            return ((CacheInfoResource)obj).Equals(this);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        #endregion

        #region Public Properties
        public Dictionary<string, CacheInfoCulture> Cultures { get; } = new Dictionary<string, CacheInfoCulture>();
        #endregion
    }

    public class CacheInfoCulture
    {
        #region Public Operators
        public static bool operator ==(CacheInfoCulture c1, CacheInfoCulture c2)
        {
            Enforce.AgainstNull(() => c1);
            Enforce.AgainstNull(() => c2);

            return c1.Equals(c2);
        }

        public static bool operator !=(CacheInfoCulture c1, CacheInfoCulture c2)
        {
            Enforce.AgainstNull(() => c1);
            Enforce.AgainstNull(() => c2);

            return !c1.Equals(c2);
        }
        #endregion

        #region Public Methods
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (!(obj is CacheInfoCulture))
                return false;

            return ((CacheInfoCulture)obj).Equals(this);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        #endregion

        #region Public Properties
        public Dictionary<string, string> Values { get; } = new Dictionary<string, string>();
        #endregion
    }
}