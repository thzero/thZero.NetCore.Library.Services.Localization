/* ------------------------------------------------------------------------- *
thZero.NetCore.Library.Services.Localization
Copyright (C) 2016-2019 thZero.com

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

using System;
using System.Globalization;

using thZero.Services;

using thZero.Configuration;

namespace thZero.Utilities
{
	public static class Culture
	{
		private static readonly thZero.Services.IServiceLog log = thZero.Factory.Instance.RetrieveLogger(typeof(Culture));

		#region Public Methods
		public static CultureInfo Get()
		{
			return Get(string.Empty);
		}

		public static CultureInfo Get(string cultureTag)
		{
			const string Declaration = "Get";

			CultureInfo culture = CultureInfo.CurrentCulture;
			try
			{
				bool found = false;
				cultureTag = DefaultCultureTag;
				if (!string.IsNullOrEmpty(cultureTag))
				{
					try
					{
						culture = new CultureInfo(cultureTag);
						found = true;
					}
					catch (Exception) { }
				}

				if (!found)
					culture = Default;
			}
			catch (Exception ex)
			{
				log.Error(Declaration, ex);
				throw;
			}

			return culture;
		}

		public static void SetDefault()
		{
			SetDefault(string.Empty);
		}

		public static void SetDefault(string defaultCultureTag)
		{
			const string Declaration = "SetDefault";

			try
			{
				if (string.IsNullOrEmpty(defaultCultureTag))
				{
					string defaultCultureTagOverride = string.Empty;
					// TODO
					//if (Utilities.Web.Configuration.Application != null)
					//	defaultCultureTagOverride = Utilities.Web.Configuration.Application.Defaults.Culture;
					if (!string.IsNullOrEmpty(defaultCultureTagOverride))
						defaultCultureTag = defaultCultureTagOverride;
				}

				bool found = false;
				if (string.IsNullOrEmpty(defaultCultureTag))
					defaultCultureTag = "en";

				if (!string.IsNullOrEmpty(defaultCultureTag))
				{
					try
					{
						Default = new CultureInfo(defaultCultureTag);
						CultureInfo.DefaultThreadCurrentCulture = Default;
						CultureInfo.DefaultThreadCurrentUICulture = Default;
						found = true;
					}
					catch (Exception) { }
				}

				if (!found)
				{
					log.Warn(Declaration, string.Concat("Unable to find the culture for '", defaultCultureTag, "'."));
					Default = CultureInfo.CurrentCulture;
				}
			}
			catch (Exception ex)
			{
				log.Error(Declaration, ex);
				throw;
			}
		}
        #endregion

        #region Public Properties
        public static CultureInfo Default { get; private set; } = CultureInfo.CurrentCulture;

        public static string Name
		{
			get
			{
				CultureInfo info = Get();
				if (info == null)
					return string.Empty;

				string name = string.Empty;
				try
				{
					name = (new CultureInfo(info.Name)).Name;
				}
				catch { }

				return name;
			}
		}
        #endregion

        #region Constants
        private const string DefaultCultureTag = "en-US";
		#endregion
	}

	public static class Localization
	{
		private static readonly thZero.Services.IServiceLog log = thZero.Factory.Instance.RetrieveLogger(typeof(Localization));

		#region Public Methods
		public static string Error(string abbreviation, params object[] args)
		{
			return StringWithResourceDefault(abbreviation, ResourceErrors, null, args);
		}

		public static string ErrorDefault(string abbreviation, string defaultValue, params object[] args)
		{
			return StringWithResourceDefault(abbreviation, ResourceErrors, defaultValue, args);
		}

		public static string Error(CultureInfo culture, string abbreviation, params object[] args)
		{
			return StringWithResourceDefault(culture, abbreviation, ResourceErrors, null, args);
		}

		public static string ErrorDefault(CultureInfo culture, string abbreviation, string defaultValue, params object[] args)
		{
			return StringWithResourceDefault(culture, abbreviation, ResourceErrors, defaultValue, args);
		}

		public static string Lookup(string abbreviation, params object[] args)
		{
			return StringWithResourceDefault(abbreviation, ResourceLookups, null, args);
		}

		public static string LookupDefault(string abbreviation, string defaultValue, params object[] args)
		{
			return StringWithResourceDefault(abbreviation, ResourceLookups, defaultValue, args);
		}

		public static string Lookup(CultureInfo culture, string abbreviation, params object[] args)
		{
			return StringWithResourceDefault(culture, abbreviation, ResourceLookups, null, args);
		}

		public static string LookupDefault(CultureInfo culture, string abbreviation, string defaultValue, params object[] args)
		{
			return StringWithResourceDefault(culture, abbreviation, ResourceLookups, defaultValue, args);
		}

		public static string Message(string abbreviation, params object[] args)
		{
			return StringWithResourceDefault(abbreviation, ResourceMessages, null, args);
		}

		public static string MessageDefault(string abbreviation, string defaultValue, params object[] args)
		{
			return StringWithResourceDefault(abbreviation, ResourceMessages, defaultValue, args);
		}

		public static string Message(CultureInfo culture, string abbreviation, params object[] args)
		{
			return StringWithResourceDefault(culture, abbreviation, ResourceMessages, null, args);
		}

		public static string MessageDefault(CultureInfo culture, string abbreviation, string defaultValue, params object[] args)
		{
			return StringWithResourceDefault(culture, abbreviation, ResourceMessages, defaultValue, args);
		}

		public static string MimeType(string abbreviation, params object[] args)
		{
			return StringWithResourceDefault(abbreviation, ResourceMimeTypes, null, args);
		}

		public static string MimeTypeDefault(string abbreviation, string defaultValue, params object[] args)
		{
			return StringWithResourceDefault(abbreviation, ResourceMimeTypes, defaultValue, args);
		}

		public static string MimeType(CultureInfo culture, string abbreviation, params object[] args)
		{
			return StringWithResourceDefault(culture, abbreviation, ResourceMimeTypes, null, args);
		}

		public static string MimeTypeDefault(CultureInfo culture, string abbreviation, string defaultValue, params object[] args)
		{
			return StringWithResourceDefault(culture, abbreviation, ResourceMimeTypes, defaultValue, args);
		}

		public static string Navigation(string abbreviation, params object[] args)
		{
			return StringWithResourceDefault(abbreviation, ResourceNavigation, null, args);
		}

		public static string NavigationDefault(string abbreviation, string defaultValue, params object[] args)
		{
			return StringWithResourceDefault(abbreviation, ResourceNavigation, defaultValue, args);
		}

		public static string Navigation(CultureInfo culture, string abbreviation, params object[] args)
		{
			return StringWithResourceDefault(culture, abbreviation, ResourceNavigation, null, args);
		}

		public static string NavigationDefault(CultureInfo culture, string abbreviation, string defaultValue, params object[] args)
		{
			return StringWithResourceDefault(culture, abbreviation, ResourceNavigation, defaultValue, args);
		}

		public static string Site(string abbreviation, params object[] args)
		{
			return StringWithResourceDefault(abbreviation, ResourceSite, null, args);
		}

		public static string SiteDefault(string abbreviation, string defaultValue, params object[] args)
		{
			return StringWithResourceDefault(abbreviation, ResourceSite, defaultValue, args);
		}

		public static string Site(CultureInfo culture, string abbreviation, params object[] args)
		{
			return StringWithResourceDefault(culture, abbreviation, ResourceSite, null, args);
		}

		public static string SiteDefault(CultureInfo culture, string abbreviation, string defaultValue, params object[] args)
		{
			return StringWithResourceDefault(culture, abbreviation, ResourceSite, defaultValue, args);
		}

		public static string Size(string abbreviation, params object[] args)
		{
			return StringWithResourceDefault(abbreviation, ResourceSize, null, args);
		}

		public static string SizeDefault(string abbreviation, string defaultValue, params object[] args)
		{
			return StringWithResourceDefault(abbreviation, ResourceSize, defaultValue, args);
		}

		public static string Size(CultureInfo culture, string abbreviation, params object[] args)
		{
			return StringWithResourceDefault(culture, abbreviation, ResourceSize, null, args);
		}

		public static string SizeDefault(CultureInfo culture, string abbreviation, string defaultValue, params object[] args)
		{
			return StringWithResourceDefault(culture, abbreviation, ResourceSize, defaultValue, args);
		}

		public static string String(string abbreviation, params object[] args)
		{
			return StringWithResourceDefault(abbreviation, null, null, args);
		}

		public static string StringDefault(string abbreviation, string defaultValue, params object[] args)
		{
			return StringWithResourceDefault(abbreviation, null, defaultValue, args);
		}

		public static string StringWithResource(string abbreviation, string resource, params object[] args)
		{
			return StringWithResourceDefault(abbreviation, resource, null, args);
		}

		public static string StringWithResourceDefault(string abbreviation, string resource, string defaultValue, params object[] args)
		{
			const string Declaration = "StringWithResourceDefault";

			try
			{
				IServiceLocalization provider = Factory.Instance.Retrieve<IServiceLocalization>();
				if (provider == null)
					return string.Concat(BracketLeft, abbreviation, BracketRight);

				CultureInfo culture = Culture.Default;
				// TODO
				//IUserProfile profile = Security.UserProfile;
				//if (profile != null)
				//	culture = profile.Culture;

				return provider.GetLocalizedStringWithResourceDefault(culture, abbreviation, resource, defaultValue, args);
			}
			catch (Exception ex)
			{
				log.Error(Declaration, ex);
				throw;
			}
		}

		public static string String(CultureInfo culture, string abbreviation, params object[] args)
		{
			return StringWithResourceDefault(culture, abbreviation, null, null, args);
		}

		public static string StringDefault(CultureInfo culture, string abbreviation, string defaultValue, params object[] args)
		{
			return StringWithResourceDefault(culture, abbreviation, null, defaultValue, args);
		}

		public static string StringWithResource(CultureInfo culture, string abbreviation, string resource, params object[] args)
		{
			return StringWithResourceDefault(culture, abbreviation, resource, null, args);
		}

		public static string StringWithResourceDefault(CultureInfo culture, string abbreviation, string resource, string defaultValue, params object[] args)
		{
			IServiceLocalization provider = Factory.Instance.Retrieve<IServiceLocalization>();
			if (provider == null)
				return string.Concat(BracketLeft, abbreviation, BracketRight);

			return provider.GetLocalizedStringWithResourceDefault(culture, abbreviation, resource, defaultValue, args);
		}

		public static string Title(string abbreviation, params object[] args)
		{
			return StringWithResourceDefault(abbreviation, ResourceTitles, null, args);
		}

		public static string TitleDefault(string abbreviation, string defaultValue, params object[] args)
		{
			return StringWithResourceDefault(abbreviation, ResourceTitles, defaultValue, args);
		}

		public static string Title(CultureInfo culture, string abbreviation, params object[] args)
		{
			return StringWithResourceDefault(culture, abbreviation, ResourceTitles, null, args);
		}

		public static string TitleDefault(CultureInfo culture, string abbreviation, string defaultValue, params object[] args)
		{
			return StringWithResourceDefault(culture, abbreviation, ResourceTitles, defaultValue, args);
		}

		public static string Validation(string abbreviation, params object[] args)
		{
			return StringWithResourceDefault(abbreviation, ResourceValidation, null, args);
		}

		public static string ValidationDefault(CultureInfo culture, string abbreviation, string defaultValue, params object[] args)
		{
			return StringWithResourceDefault(culture, abbreviation, ResourceValidation, defaultValue, args);
		}
		#endregion

		#region Constants
		private const string BracketLeft = "[";
		private const string BracketRight = "}";
		private const string ResourceErrors = "errors";
		private const string ResourceLookups = "lookups";
		private const string ResourceMessages = "messages";
		private const string ResourceMimeTypes = "mimeTypes";
		private const string ResourceNavigation = "navigation";
		private const string ResourceSite = "site";
		private const string ResourceSize = "sizes";
		private const string ResourceTitles = "titles";
		private const string ResourceValidation = "validation";
		#endregion
	}
}