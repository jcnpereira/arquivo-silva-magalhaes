using ArquivoSilvaMagalhaes.Utilitites;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ArquivoSilvaMagalhaes.Models
{
    public class CodeGenerator
    {
        /// <summary>
        /// Comparator which compares numeric strings in a natural way.
        /// For example, 1 < 1a < 2 < 10a
        /// </summary>
        private readonly static AlphaNumericComparator _comparator = new AlphaNumericComparator();

        /// <summary>
        /// Suggests a document code for the specified
        /// collection, in the format COL-DOC
        /// 
        /// If no collection exists with that id, null
        /// is returned.
        /// </summary>
        public static string SuggestDocumentCode(int collectionId)
        {

            using (var db = new ArchiveDataContext())
            {
                var c = db.Collections.Find(collectionId);

                if (c == null)
                {
                    return null;
                }

                var collectionCode = c.CatalogCode; // COL

                var docCodes = c.Documents.Select(d => d.CatalogCode).ToArray();

                return SuggestCode(collectionCode, docCodes);
            }
        }

        /// <summary>
        /// Suggests an image code for the specified
        /// document, the format COL-DOC-IMG
        /// 
        /// If no document exists with that id, null
        /// is returned.
        /// </summary>
        public static string SuggestImageCode(int documentId)
        {
            using (var db = new ArchiveDataContext())
            {
                var d = db.Documents.Find(documentId);

                if (d == null)
                {
                    return null;
                }

                var docCode = d.CatalogCode; // COL-DOC
                var imageCodes = d.Images.Select(i => i.ImageCode).ToArray();

                return SuggestCode(docCode, imageCodes);
            }
        }

        public static string SuggestCode(string baseCode, IList<string> existingCodes)
        {
            if (!String.IsNullOrWhiteSpace(baseCode))
            {
                baseCode += "-"; // Eg: "COL" -> "COL-"
            }

            if (existingCodes.Count != 0)
            {
                // Extract the last component from the existing codes.
                // Example: "ASM-001-009" -> "009"
                var lastComponents =
                    existingCodes.Select(c => c.Split('-').Last()).ToArray();

                // Sort the codes.
                Array.Sort(lastComponents, _comparator);

                var lastCode = lastComponents.Last();
                int lastCodeNumeric;

                
                if (int.TryParse(lastCode, out lastCodeNumeric))
                {
                    // Last code is a number. Add 1 to it and return.
                    return baseCode + (lastCodeNumeric + 1);
                }
                else
                { 
                    // Last code is not a number (eg. "8a"). Therefore,
                    // we're going to take the number of elements,
                    // and then try to find a gap in the array.
                    // We do this because the count can be, 8, and "8"
                    // already exists in the code list, for example.
                    var count = lastComponents.Count();
                    var increment = 0;

                    while (lastComponents.Contains((count + increment).ToString()))
                    {
                        increment++;
                    }

                    return baseCode + (count + increment);
                }
            }

            // No codes available. Return the base code plus 1.
            return baseCode + "1";
        }

        public static string SuggestSpecimenCode(int imageId)
        {
            using (var db = new ArchiveDataContext())
            {
                var i = db.Images.Find(imageId);

                if (i == null)
                {
                    return null;
                }

                var imageCode = i.ImageCode; // COL-DOC-IMG
                var specimenCodes = i.Specimens.Select(s => s.ReferenceCode).ToArray();

                return SuggestCode(imageCode, specimenCodes);
            }
        }
    }
}