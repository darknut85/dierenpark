using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dierenpark
{
    class Program
    {
        static void Main(string[] args)
        {
            //data not in constructor
            DateTime register;
            Original original = new Original(DateTime.MinValue,"",0,0,0,0,0,0,0,0,0);

            //data in constructor
            string response = "yes";
            DateTime age = original.getAges();
            string names = original.getName();
            int dateDifferences = original.getDateDifference();
            int kidd = original.getKidd();
            int grown = original.getGrown();
            int old = original.getOlder();
            int coupl = original.getCoupl();
            int oldCoupl = original.getOldCoupl();
            int total = original.getTotal();
            int discount = original.getDiscount();
            int fkidd = original.getFkidd();

            //registration date
            Console.WriteLine("What is the registration date?");
            while (true)
            {
                try
                {
                    register = Convert.ToDateTime(Console.ReadLine());
                    break;
                }
                catch (Exception)
                {
                    Console.WriteLine("Invalid. Try again.");
                }
            }

            //calculation for entering and discount for grown-ups
            while (response != "no")
            {
                //registration input
                Console.WriteLine("Who do you wish to register?");
                names = Console.ReadLine();
                Console.WriteLine("What is the birthdate of the person you wish to register?");
                while (true)
                {
                    try
                    {
                        age = Convert.ToDateTime(Console.ReadLine());
                        break;
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Invalid. Try again.");
                    }
                }

                //calculate age
                double comDate = (register - age).TotalDays;
                dateDifferences = Convert.ToInt32(comDate)/365;
                Console.WriteLine("The registered person's age is: " + dateDifferences);

                //kid price
                if (dateDifferences < 13)
                {
                    fkidd++;
                    kidd += 11;
                    total += 11;
                }

                //grown-up price
                else if (dateDifferences >= 13 && dateDifferences < 65)
                {
                    coupl++;
                    grown += 30;
                    total += 30;

                    //grown-up discount
                    if (coupl == 1 && oldCoupl == 1)
                    {
                        discount = discount + 10;
                    }
                    if (coupl == 2 && oldCoupl == 0)
                    {
                        discount = discount + 10;
                    }
                }

                //elderly price
                else
                {
                    oldCoupl++;
                    old += 26;
                    total += 26;

                    //elderly discount
                    if (coupl == 1 && oldCoupl == 1)
                    {
                        discount = discount + 10;
                    }
                    if (oldCoupl == 2 && coupl == 0)
                    {
                        discount = discount + 12;
                    }
                }

                // repeat registration if not no
                Console.WriteLine(" Would you like to register another person?");
                response = Console.ReadLine();
            }

            // kid discount. (depends on exactly 2 elderly and grown-ups together)
            if (fkidd == 1 && ((coupl + oldCoupl) == 2))
            {
                discount = discount + 6;
            }
            if (fkidd >= 2 && ((coupl + oldCoupl) == 2))
            {
                discount = discount + 11;
            }

            //second construct obtains data. Didn't use all of it
            Zoo zoo = new Zoo(names, dateDifferences, grown, kidd, old, coupl, oldCoupl, fkidd, total, discount);
            
            Console.WriteLine(zoo.allCost());
            Console.WriteLine(zoo.finalCost());
            Console.WriteLine(zoo.discountGain());
        }
    }
    public class Zoo
    {
        string names;
        int age;
        int grownUp;
        int kid;
        int old;

        //The values are altered from the original challenge, because the alternative is always cheaper
        int couple; //more expensive than 2 1-person subscriptions, gave them 10 discount instead, if there are exactly 2 grown-ups
        int oldCouple; //more expensive than 2 1-person subscriptions, gave them 8 discount instead, if there are at least 2 65+
        int FKid; //same as 2 normal tickets + 1 kid, giving 6 discount instead, if at least 2 kids, 11 discount instead.(there must be exactly 2 grown-ups)


        int totale;
        int discountt;
        //constructor
        public Zoo(string names, int age, int grownUp, int kid, int old, int couple, int oldCouple, int FKid, int totale, int discountt)
        {
            this.names = names;
            this.age = age;
            this.grownUp = grownUp;
            this.kid = kid;
            this.old = old;
            this.couple = couple;
            this.oldCouple = oldCouple;
            this.FKid = FKid;
            this.totale = totale;
            this.discountt = discountt;
        }

        //returned values
        public string getNames(){return names;}
        public int getAge(){return age;}
        public int getGrownUp(){return grownUp;}
        public int getKid(){return kid;}
        public int getOld(){return old;}
        public int getCouple() { return couple; }
        public int getOldCouple() { return oldCouple; }
        public int getFKid() { return FKid; }
        public int getTotale() { return totale; }
        public int getDiscountt() { return discountt; }

        
        public string allCost()
        {
            return (" The total cost for kids is: " + getKid()
                + " The total cost for grown-ups is: " + getGrownUp()
                + " The total cost for 65+ is: " + getOld());
        }
        public string finalCost()
        {
            return ("the total cost is: " + (getTotale() - getDiscountt()));
        }
        public string discountGain()
        {
            return ("Your discount is: " + getDiscountt());
        }
    }
    public class Original
    {
        DateTime ages;
        string name;
        int discount;
        int total;
        int kidd;
        int grown;
        int older;
        int coupl;
        int oldCoupl;
        int fkidd;
        int dateDifference;

        //constructor
        public Original(DateTime ages,string name, int discount, int total, int kidd, int grown, int older, int coupl, int oldCoupl, int fkidd, int dateDifference)
        {
            this.ages = ages;
            this.name = name;
            this.discount = discount;
            this.total = total;
            this.kidd = kidd;
            this.grown = grown;
            this.older = older;
            this.coupl = coupl;
            this.oldCoupl = oldCoupl;
            this.fkidd = fkidd;
            this.dateDifference = dateDifference;
        }
        //returned values
        public DateTime getAges() { return ages; }
        public string getName() { return name; }
        public int getDiscount() { return discount; }
        public int getTotal(){ return total; }
        public int getKidd() { return kidd; }
        public int getGrown() { return grown; }
        public int getOlder() { return older; }
        public int getCoupl() { return coupl; }
        public int getOldCoupl() { return oldCoupl; }
        public int getFkidd() { return fkidd; }
        public int getDateDifference() { return dateDifference; }
    }
}
