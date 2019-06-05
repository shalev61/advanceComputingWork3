//Shalev Goldshtein 212210280
using System;

using System.Text;


using System.Collections.Generic;
using System.Threading.Tasks;

using System.Linq;

namespace Excercise_1
{

    public class FunctionsContainer
    {
        private Dictionary<string, Func<double, double>> dictionFunc;



        /*
         * get list of the keys
         */
        public List<string> getAllMissions()
        {
            //the method doing list from the keys dictionary
            return dictionFunc.Keys.ToList();
        }


        /*
         * the mission doing
         */
        public Func<double, double> this[string someFnc]
        {
            set
            {
                dictionFunc[someFnc] = value;
            }

            //the get func of the class
            get
            {
                //checking the func in the diction
                if (false == dictionFunc.ContainsKey(someFnc))
                {
                    //intilize by falte
                    dictionFunc[someFnc] = val => val;

                }

                //return end calc
                return new Func<double, double>(dictionFunc[someFnc]);
            }


        }

        /*
         * the constarctor of the class
         */
        public FunctionsContainer()
        {
            //initliize in c#
            this.dictionFunc = new Dictionary<string, Func<double, double>>();
        }

    }
}

