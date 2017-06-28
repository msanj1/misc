using System;
using System.Linq;
using System.Linq.Expressions;

public partial class Inheritance : System.Web.UI.Page
{
    protected override void OnLoad(EventArgs e)
    {

        Expression<Func<Animal, bool>> x = animal => animal.Name == "Fido";

        var connectionString = "server=.;database=animals;integrated security=true;";
        using(AnimalsDataContext dc = new AnimalsDataContext(connectionString))
            {
                dc.CreateDatabase();

                try
                {
                    Cat cat = new Cat {Name = "Felix", FelineDistemperShot = true};
                    Dog dog = new Dog {Name = "Fido", KennelClubMember = true};
                    
                    dc.Animals.InsertOnSubmit(dog);
                    dc.Animals.InsertOnSubmit(cat);
                    dc.SubmitChanges();

                    var query = dc.Animals
                                  .Where(animal => animal.Name == "Fido");

                }
                finally
                {
                    dc.DeleteDatabase();
                }
        }
    }
}
