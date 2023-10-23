1.Thread: birbirinden bagimsiz workflowlar.

2.Fakat Python3 uygulamalarinda threadler genelde ayni anda calismazlar.

3.Iki farkli thread iki farkli islemci uzerindeki workflowlarda calisir. Fakat her thread kendi islemcisi uzerinde siraya gore calisir. Bir islemci uzerinde iki thread ayni anda calismaz.

4.Pythonda ayni anda birden isi yapmak icin yazilacak bir kodun implemente edilmesi icin ya kodun bir kismini farkli bir dilde yazmak ya da <font color="red"> multiprocessing </font> kullanmak gerekir.

5. Genelde disaridan beklenen cevaplarda thread kullanmak mantiklidir.

6. Programlamayi threadlar kullanarak yapmak kodun okunabilirligini arttirmak, daha temiz bir kod yazmak gibi iyi ozelliklere sahip dizayn avantajlari saglar.

## SORUNLAR

1. Cok fazla islemci mesguliyeti olusturur.
2. Az beklenen external isteklerde ciddi bir hiz farki olusturmayabilir.



## DAEMON PROCESS
1. Bilgisayar biliminde DAEMON demek arka tarafta calisan bir job demektir. 
2. Fakat python dilindeki thread mantiginda DAEMON demek, program calismayi durdurdugu anda kendisi de calismayi durduran ve son bulan joblardir. 
3. Eger bir process DAEMON degilse, proje kapatildiginda once threadlerin bitmesi beklenir. 
4. Kisacasi DEAMON threadler bir proje kapatildiginda kapanmasindan endise etmedigimiz thread yapilarini ifade eder.

## JOIN() Keyword
1. threading__shutdown() adli fonksiyon, DAEMONIC olmayan butun threadleri dolasip hepsinde JOIN() metodunu cagirir.
2. Boylece butun threadler bittiginde program kapanmaya hazir olur.

## JOIN() keywordunun esas amaci
1. Bir threade, bitmek icin baska bir threadi beklemesi gerektigini belirtmek icin kullaniriz.

Orn: x.join(), x threadinin bitmesini bekle!

## THREAD POOL EXECUTER NEDIR?

1. ThreadPoolExecuter Pythonda, birden cok threadi bir anda baslatabilmek icin kullanilan bir yapidir. 
2. concurrent.futures adli standart kutuphanden gelmektedir. 
3. with statement ile beraber kullanmak best practice dir.


# RACE CONDITIONS
1. Thread kullanilan programlarda karsilasilan zorlayici problemlerden birisi.
## RACE CONDITION NEDIR?
1. Race condition durumu iki veya daha fazla threadin paylasilmis bir dataya erismeye calismasinda ortaya cikar.
2. Race condition durumlari nadiren gerceklesir ve cok bariz yapilar degillerdir. Bu nedenle debug etmesi de zordur.