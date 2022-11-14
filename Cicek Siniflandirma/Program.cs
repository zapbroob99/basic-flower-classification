using Cicek_Siniflandirma.Properties;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Cicek_Siniflandirma
{
    internal class Program
    {
        static void Main(string[] args)
        {
            NeuralNetwork network = new NeuralNetwork();
            network.datasetAl(Resources.iris);
            network.onaBol();
            network.init_train_test(0.01, 50); 
            network.init_train_test(0.01, 20);
            network.init_train_test(0.01, 100);
            network.init_train_test(0.025, 20);
            network.init_train_test(0.025, 50);
            network.init_train_test(0.025, 100);
            network.init_train_test(0.005, 20);
            network.init_train_test(0.005, 50);
            network.init_train_test(0.005, 100);
            //DENEYLER TEKRARLANIYOR
            network.init_train_test(0.01, 50); 
            network.init_train_test(0.01, 20);
            network.init_train_test(0.01, 100);
            network.init_train_test(0.025, 20);
            network.init_train_test(0.025, 50);
            network.init_train_test(0.025, 100);
            network.init_train_test(0.005, 20);
            network.init_train_test(0.005, 50);
            network.init_train_test(0.005, 100);
            //DENEYLER TEKRARLANIYOR
            network.init_train_test(0.01, 50); 
            network.init_train_test(0.01, 20);
            network.init_train_test(0.01, 100);
            network.init_train_test(0.025, 20);
            network.init_train_test(0.025, 50);
            network.init_train_test(0.025, 100);
            network.init_train_test(0.005, 20);
            network.init_train_test(0.005, 50);
            network.init_train_test(0.005, 100);        
        }
    }
    class NeuralNetwork
    {

        Neuron noron1 = new Neuron("Iris-setosa");
        Neuron noron2 = new Neuron("Iris-versicolor");
        Neuron noron3 = new Neuron("Iris-virginica");
        //proje için 3 adet nöron gerekmekte
        public double[,] inputSet = new double[150, 4]; //inputları içeren bir matrix oluşturuluyor
        public string[] outputSet = new string[150]; //outputlar
        public double[,] weights = new double[3, 4];

        public void datasetAl(string rawdata) //kullanılacak input ve outputları inputSet ve outputSet objesine aktaran fonksiyon
        {
            
            using (var stringreader = new StringReader(rawdata))
                for (int i = 0; i < 150; i++)
                {
                    String line = stringreader.ReadLine();
                    for (int j = 0; j < 5; j++)
                    {

                        {

                            if (line != null)
                            {
                                if (j == 4)
                                {
                                    outputSet[i] = line.Split(',')[j];
                                }
                                else
                                {
                                    inputSet[i, j] = Convert.ToDouble(line.Split(',')[j]);
                                }



                            }
                        }

                    }
                }

        }
        public void initWeights() //ağırlıkları en başta 0.0 ile 1.0 arasındaki double değerlerine rastgele atar.
        {
            Random rnd = new Random();
            double[,] initialWeights = new double[3, 4];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 4; j++)
                {   
                    initialWeights[i, j] = rnd.NextDouble();
                }
            }
            this.weights = initialWeights;
        }
        public void onaBol()
        {
            for (int i = 0; i < 150; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    inputSet[i, j] = inputSet[i, j] / 10;
                }
            }
        }
        public double[] slice(double[,] original, int index)
        {
            double[] arr = new double[4];
            for (int i = 0; i < 4; i++)
            {
                arr[i] = original[index, i];
                
            }
            return arr;
        }

        public void train(double l_multiplier, int loopNumber)//l mutliplier=öğrenme katsayısı
        {
            noron1.weights = slice(weights, 0);
            noron2.weights = slice(weights, 1);
            noron3.weights = slice(weights, 2);
            double trueGuess = 0;
            for (int k = 0; k < loopNumber; k++)
            {
                double guess = 0;
                for (int i = 0; i < 150; i++)
                {
                    
                    double max ;
                    Neuron outputNeuron;
                    Neuron expectedNeuron;
                    
                    double[] currentInput = slice(inputSet, i); 
                    noron1.toplam = noron1.topla(noron1.weights, currentInput); 
                    noron2.toplam = noron2.topla(noron2.weights, currentInput);
                    noron3.toplam = noron3.topla(noron3.weights, currentInput); 
                    string expectedOutput = outputSet[i];
                    
                    if (noron1.toplam > noron2.toplam)
                    {
                        
                        if (noron1.toplam > noron3.toplam)
                        {
                            
                            outputNeuron = noron1;
                        }
                        else
                        {
                            
                            outputNeuron = noron3;
                        }
                    }
                    else if (noron2.toplam > noron3.toplam)
                    {
                        
                        outputNeuron = noron2;
                    }
                    else
                    {
                        
                        outputNeuron = noron3;
                    }
                    
                    if (expectedOutput.Equals(outputNeuron.type))
                    {
                        trueGuess++;// Console.WriteLine("Beklenen: "+ expectedOutput + "Çıktı:" + outputNeuron.type+ "Dogruluk: true");
                        guess++;
                        
                       
                    }
                    else
                    {
                        //Console.WriteLine("Beklenen: " + expectedOutput + "Çıktı:" + outputNeuron.type + "Dogruluk: false");
                        expectedNeuron = noron1;
                        if (noron2.type.Equals(expectedOutput))
                        {
                            expectedNeuron = noron2;
                        }
                        if (noron1.type.Equals(expectedOutput))
                        {
                            expectedNeuron = noron1;
                        }
                        else if (noron3.type.Equals(expectedOutput))
                        {
                            expectedNeuron = noron3;
                        }
                        for (int j = 0; j < outputNeuron.weights.Length; j++)
                        {
                           
                            outputNeuron.weights[j] = outputNeuron.weights[j] - (l_multiplier * currentInput[j]); //w = w – (λ * x)                           
                            expectedNeuron.weights[j] = expectedNeuron.weights[j] + (l_multiplier * currentInput[j]); //w = w + (λ * x)
                        }
                        
                    }
                    if (i > 0)
                    {
                        
                    }
                    
                }
               
            }
            Console.WriteLine("Learning Rate: "+l_multiplier+" , Epochs: "+ loopNumber);

        }
        public double test()
        {
            double trueGuess = 0;
            for (int i = 0; i < 150; i++)
            {
                Neuron outputNeuron;
                Neuron expectedNeuron=null;

                double[] currentInput = slice(inputSet, i);
                noron1.toplam = noron1.topla(noron1.weights, currentInput);
                noron2.toplam = noron2.topla(noron2.weights, currentInput);
                noron3.toplam = noron3.topla(noron3.weights, currentInput);
                string expectedOutput = outputSet[i];
                if (noron1.toplam > noron2.toplam)
                {

                    if (noron1.toplam > noron3.toplam)
                    {

                        outputNeuron = noron1;
                    }
                    else
                    {

                        outputNeuron = noron3;
                    }
                }
                else if (noron2.toplam > noron3.toplam)
                {

                    outputNeuron = noron2;
                }
                else
                {

                    outputNeuron = noron3;
                }
                if (expectedOutput.Equals(outputNeuron.type))
                {
                    trueGuess++; 
                    


                }
                else
                {
                    
                }
            }
            Console.WriteLine("test accuracy: " + (100 * trueGuess / 150) + "%");
            Console.WriteLine("-------------------");
            return  (100*trueGuess / 150);
            
        }
        public void init_train_test(double l_multiplier, int loopNumber)
        {
            initWeights();
            train(l_multiplier, loopNumber);
            test();
        }

    }
    class Neuron
    {
        public double[] weights;
        public double toplam;
        public string type;
        public double topla(double[] weights, double[] inputs)
        {
            double toplam = 0;
            int count = 0;
            foreach (var input in inputs)
            {
                toplam = toplam + input * weights[count];
                count++;
            }
            return toplam;
        }
        public Neuron(string type)
        {
            this.type = type;
        }
    }
}