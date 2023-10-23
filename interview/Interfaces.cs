public interface ITaxCalculator{
    int Calculate(); //body yok, implementasyanu yok, access modifier yok
    //amac: loosely-coupled applicationlar üretmek icin kullanılır,
    //tightly coupled yerine loosely coupled.

    //sürdürülebilir yazılım yapılmasını sağlar.
}


class CustomerManager
{
   
    //spaghetti code.
    public void Add(){
        int mevzuat = 1;
        if(mevzuat == 1){ //single responsibility kuralını da ciginiyoruz, aynı islemin iki farklı versiyonunu aynı yerde yazıyoruz.
            System.Console.WriteLine("Musteri birinci mevzuata gore eklendi");   
        }
        else {
            System.Console.WriteLine("musteri ikinci mevzuata gore eklendi");
        }
     
    }
}


interface IMevzuat{
    void islemYap();
}