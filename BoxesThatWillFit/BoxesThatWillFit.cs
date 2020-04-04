using System;

namespace BoxesThatWillFit
{
    class BoxesThatWillFit
    {
        static void Main(string[] args)
        {
            static double Divide(Box bigBox, Box smallBox)
            {
                if (bigBox.volume < smallBox.volume)
                {
                    Console.WriteLine("- 'Big Box' SMALLER THAN 'Small Box");
                    return 0;
                }
                else
                {
                    double inWidth = Math.Truncate((bigBox.width / smallBox.width));
                    double inLength = Math.Truncate((bigBox.length / smallBox.length));
                    double inHeight = Math.Truncate((bigBox.height / smallBox.height));

                    double inMainChunk = inWidth * inLength * inHeight;

                    if(inWidth*smallBox.width < bigBox.width)
                    {
                        Box northChunk = new Box(bigBox.width - (inWidth * smallBox.width),
                                      inLength * smallBox.length,
                                      bigBox.height, "north");

                        northChunk.height = northChunk.dims[2];
                        northChunk.length = northChunk.dims[1];
                        northChunk.width = northChunk.dims[0];

                        //Divide NorthChunk:
                        double inWidthNC = Math.Truncate((northChunk.width / smallBox.width));
                        double inLengthNC = Math.Truncate((northChunk.length / smallBox.length));
                        double inHeightNC = Math.Truncate((northChunk.height / smallBox.height));

                        inMainChunk += inWidthNC * inLengthNC * inHeightNC;
                    }
                    
                    if(inLength*smallBox.length < bigBox.length)
                    {
                        Box southChunk = new Box(bigBox.width,
                                      bigBox.length - (inLength * smallBox.length),
                                      bigBox.height, "south");

                        southChunk.height = southChunk.dims[2];
                        southChunk.width = southChunk.dims[1];
                        southChunk.length = southChunk.dims[0];
                        
                        //Divide SouthChunk:
                        double inWidthSC = Math.Truncate((southChunk.width / smallBox.width));
                        double inLengthSC = Math.Truncate((southChunk.length / smallBox.length));
                        double inHeightSC = Math.Truncate((southChunk.height / smallBox.height));

                        inMainChunk += inWidthSC * inLengthSC * inHeightSC;
                    }
                    
                    return inMainChunk;
                }
            }

            Console.WriteLine("ENTER BIG BOX DIMS (FORMAT: 'x,y,z'):");
            string[] bbi = Console.ReadLine().Split(",");
            double[] BBdims = new double[3];
            for(int ind = 0; ind < bbi.Length; ind++)
            {
                BBdims[ind] = Convert.ToDouble(bbi[ind]);
            }

            Console.WriteLine("ENTER SMALL BOX DIMS (FORMAT: 'x,y,z'):");
            string[] sbi = Console.ReadLine().Split(",");
            double[] SBdims = new double[3];
            for (int ind = 0; ind < sbi.Length; ind++)
            {
                SBdims[ind] = Convert.ToDouble(sbi[ind]);
            }

            Box b1 = new Box(BBdims[0], BBdims[1], BBdims[2], "big");
            Box b2 = new Box(SBdims[0], SBdims[1], SBdims[2], "small");

            Console.WriteLine("\n" + Divide(b1, b2).ToString() +
                " Boxes of dimensions:\nH:{0} L:{1} W:{2}\nWill fit inside a box of dimensions:\n" +
                "H:{3} L:{4} W:{5}" , b2.height, b2.length, b2.width, b1.height, b1.length, b1.width);

        }
    }
    class Box
    {
        public double height { get; set; }
        public double width { get; set; }
        public double length { get; set; }
        public double volume { get; set; }
        public double[] dims = new double[3];
        public Box(double d1, double d2, double d3, string identifier = "neutral")
        {
            dims[0] = d1; dims[1] = d2; dims[2] = d3;
            for(int cycles = 0; cycles < dims.Length; cycles++)
            {
                for (int dim = 0; dim != 2; dim++)
                {
                    if (dims[dim] > dims[dim + 1])
                    {
                        double temp = dims[dim + 1];
                        dims[dim + 1] = dims[dim];
                        dims[dim] = temp;
                    }
                }
            }

            if (identifier == "big")
            {
                //Largest side must be height
                height = dims[2];
                width = dims[1];
                length = dims[0];
            }
            if(identifier == "small")
            {
                //smallest side must be height
                height = dims[0];
                width = dims[2];
                length = dims[1];
            }
            if(identifier == "north")
            {
                //Largest must be height
                //Second largest must be Length
                height = dims[2];
                width = dims[0];
                length = dims[1];
            }
            if(identifier == "south")
            {
                //Largest side must be height
                //Second largest must be width
                height = dims[2];
                width = dims[1];
                length = dims[0];
            }
            volume = width * height * length;

        }  
    }
}
