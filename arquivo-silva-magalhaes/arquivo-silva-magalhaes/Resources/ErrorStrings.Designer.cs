﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ArquivoSilvaMagalhaes.Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class ErrorStrings {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ErrorStrings() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("ArquivoSilvaMagalhaes.Resources.ErrorStrings", typeof(ErrorStrings).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Não pode eliminar informações textuais na linguagem pré-definida..
        /// </summary>
        public static string CannotDeleteDefaultLang {
            get {
                return ResourceManager.GetString("CannotDeleteDefaultLang", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to A data de falecimento tem que ser posterior à data de nascimento..
        /// </summary>
        public static string DeathDateEarlierThanBirthDate {
            get {
                return ResourceManager.GetString("DeathDateEarlierThanBirthDate", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to A data de fim não pode ser anterior à data de início..
        /// </summary>
        public static string EndMomentEarlierThanStartMoment {
            get {
                return ResourceManager.GetString("EndMomentEarlierThanStartMoment", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to A data de expiração não pode ser anterior à data de criação..
        /// </summary>
        public static string ExpiryDateEarlierThanPublishDate {
            get {
                return ResourceManager.GetString("ExpiryDateEarlierThanPublishDate", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to O código de imagem especificado já existe. Por favor, escolha outro..
        /// </summary>
        public static string Image__CodeAlreadyExists {
            get {
                return ResourceManager.GetString("Image__CodeAlreadyExists", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Foi impossível encontrar o documento solicitado..
        /// </summary>
        public static string Image__UnknownDocument {
            get {
                return ResourceManager.GetString("Image__UnknownDocument", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Data inválida..
        /// </summary>
        public static string InvalidDate {
            get {
                return ResourceManager.GetString("InvalidDate", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to O logótipo tem que ser um ficheiro de imagem..
        /// </summary>
        public static string Logo__MustBeImage {
            get {
                return ResourceManager.GetString("Logo__MustBeImage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Tem que especificar qual é o conteúdo que pretende modificar ou eliminar..
        /// </summary>
        public static string MustSpecifyContent {
            get {
                return ResourceManager.GetString("MustSpecifyContent", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Foi impossível encontrar a imagem solicitada..
        /// </summary>
        public static string Specimen__UnknownImage {
            get {
                return ResourceManager.GetString("Specimen__UnknownImage", resourceCulture);
            }
        }
    }
}
