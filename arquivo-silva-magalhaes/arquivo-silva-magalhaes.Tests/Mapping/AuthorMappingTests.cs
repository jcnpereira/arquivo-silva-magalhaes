//using System;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using ArquivoSilvaMagalhaes.Models.ArchiveModels;
//using ArquivoSilvaMagalhaes.Models.ArchiveViewModels;
//using AutoMapper;

//namespace ArquivoSilvaMagalhaes.Tests.Mapping
//{
//    [TestClass]
//    public class AuthorMappingTests
//    {
//        [TestMethod]
//        public void TestAuthorMapping()
//        {
//            var a = new AuthorEditViewModel
//            {
//                BirthDate = new DateTime(2000, 1, 1),
//                DeathDate = new DateTime(2001, 1, 1),
//                FirstName = "Primeiro",
//                LastName = "Ultimo"
//            };

//            Mapper.CreateMap<AuthorEditViewModel, Author>()
//                  .ConvertUsing(vm => new Author
//                  {
//                      FirstName = vm.FirstName,
//                      LastName = vm.LastName,
//                      BirthDate = vm.BirthDate,
//                      DeathDate = vm.DeathDate
//                  });

//            var author = Mapper.Map<Author>(a);

//            Assert.AreEqual(a.FirstName, author.FirstName);


//        }
//    }
//}
