using Microsoft.Xrm.Sdk;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests {
    [TestClass]
    public class Example {
        public Entity CreatedRegister;
        public Tester Tester = new Tester();

        [TestMethod]
        public void ExampleTest() {
            CreateRegister();
            UpdateRegister();
            DeleteRegister();
        }

        public void CreateRegister() {
            CreatedRegister = new Entity("account");
            CreatedRegister["name"] = "Account Example";

            Assert.IsTrue(Tester.Create(CreatedRegister), "Unable to create entity");

        }

        public void UpdateRegister() {
            CreatedRegister["name"] = "Account Example Updated";
            Assert.IsTrue(Tester.LogError(true).Update(CreatedRegister), "Unable to update entity");
        }

        public void DeleteRegister() {
            Assert.IsTrue(Tester.Delete(CreatedRegister), "Unable to delete entity");
        }
    }
}