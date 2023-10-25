using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOrganization
{
    internal abstract class Organization
    {
        private Position root;

        public Organization()
        {
            root = CreateOrganization();
        }

        protected abstract Position CreateOrganization();

        /**
         * hire the given person as an employee in the position that has that title
         * 
         * @param person
         * @param title
         * @return the newly filled position or empty if no position has that title
         */
        public Position? Hire(Name person, string title)
        {
            //your code here
            var positionsList = new List<string>();
            var resultPos = getPositions(positionsList, this.root, person, title);
            return resultPos;

        }

        public Position? getPositions(List<string> positions, Position value, Name person, string title)
        {
            positions.Add(value.ToString());
            if (value.ToString() == title)
            {
                value.SetEmployee(new Employee(1, person));
                return value;
            }
            else if (value.GetDirectReports().Count > 0)
            {
                foreach (var item in value.GetDirectReports())
                {
                    getPositions(positions, item, person, title);
                }
            }
            return null;
        }

        override public string ToString()
        {
            return PrintOrganization(root, "");
        }

        private string PrintOrganization(Position pos, string prefix)
        {
            StringBuilder sb = new StringBuilder(prefix + "+-" + pos.ToString() + "\n");
            foreach (Position p in pos.GetDirectReports())
            {
                sb.Append(PrintOrganization(p, prefix + "  "));
            }
            return sb.ToString();
        }
    }
}
