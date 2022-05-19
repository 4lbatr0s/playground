namespace playground
{
    public class Program{
        public static void Main(){

            //garbage collector: bir referansı tutan hiçbir tanım kalmadıysa, o referans bellekten uçurulur.

            //value types: int, double, enum, boolean, sayısal degerler.
            int sayi; //stackte tanımlanıyor
            sayi = 10; //stackte tutuluyor.
            int sayi2 = sayi; //sayi 2nin degeri sayi 1 in degeridir. adrese işaret etmez.


            //arraylar referans tiptir, interface, abstract class, class referans tiplerdir. 
            //string: referans tiptir, çalışma şekli değer tip gibidir.. dene gör.
            string [] sehirler1 = new string []{"ankara", "istanbul", "izmir"}; //sehirler1 stack içerisinde tanımlandı
            string [] sehirler2 = new string []{"adana", "bursa", "bolu"};
        }



    }
}