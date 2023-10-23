'use strict';

//INFO: In this module, we are going to study Objects, Destructuring, Spread,Rest, New Syntax

//1.Shorthand Properties
let name = 'alice',
    age = 10;
const alice = {
    name: name,
    age: age,
};
const aliceShorthand = {
    name,
    age,
};

//INFO: 2.Computed Property Names
// let prop1 = "name";
// let myObj = {}
// myObj[prop1]="alice";
// console.log(myObj);
//TIP:
let prop1 = 'name';
let myObj = {
    [prop1]: 'alice',
};
console.log(myObj);

//INFO:3.SHORT METHOD SYNTAX
/* const person = {
    name: 'John',
    surname: 'Doe',
    age: 40,
    fullName: function() {
        return this.name + " " + this.surname
    }
} */
//TIP:
const person = {
    name: 'John',
    surname: 'Doe',
    age: 40,
    fullName() {
        return this.name + ' ' + this.surname;
    },
};

//INFO:4.OBJECT DESTRUCTING
// let myVar1=person.name;
// let myVar2=person.age;
//TIP:instead of this, use below:
let { name: myVar1, age: myVar2 } = person;
console.log(myVar1, myVar2);
//TIP:
// let { name: name, age: age } = person;
// console.log(name);
// console.log(age);
//TIP:
// let { name, age } = person;
// console.log(name);
// console.log(age);



//INFO:5. HOW TO GET PARTIAL DATA FROM OBJECTS!
const books = [
    {
        title: 'Kırmızı Pazartesi',
        author: 'Haruki Murakami',
        pageNum: 296,
        imageURL:
            'https://i.idefix.com/cache/600x600-0/originals/0000000064101-1.jpg',
        topic: '1968-1970 yılları arasında geçen olaylar, o günün toplumsal gerçeklerini de satırlara taşıyor. Ama romanın odağında bu toplumsal olaylar değil üçlü bir aşk var. Gençliğin rüzgârıyla hareketlenen İmkânsızın Şarkısını ölümle erken karşılaşan gençlerin hayatı yönlendiriyor. Hiçbir şeyin önem taşımadığı, amaçsızlığın ağır bastığı, özgür seksin kol gezdiği bir öğrenci hayatı... Ama diğer yanda da yoğun duygular var... İmkânsız aşklar, imkânsız şarkılar söyleten. Hemen hemen her Japon gencinin okuduğu roman anayurdu dışında da çok kişi tarafından sahipleniliyor.',
    },
    {
        title: 'Şeker Portakalı',
        author: 'Jose Muro de Vasconselos',
        pageNum: 200,
        imageURL:
            'https://i.idefix.com/cache/600x600-0/originals/0000000064031-1.jpg',
        topic: 'İrlandalı yazar Bram Stoker’ın, iki taraf arasındaki bu irade ve güç çatışmasını işlediği ve korku edebiyatının başyapıtlarından biri sayılan Dracula, yayımlanmasının üzerinden yüz yılı aşkın süre geçmesine karşın, bugün de aynı ilgiyle okunuyor.',
    },
    {
        title: 'En Uzun Yüzyıl',
        author: 'İlber Ortaylı',
        pageNum: 296,
        imageURL:
            'https://i.idefix.com/cache/600x600-0/originals/0001744876001-1.jpg',
        topic: '1968-1970 yılları arasında geçen olaylar, o günün toplumsal gerçeklerini de satırlara taşıyor. Ama romanın odağında bu toplumsal olaylar değil üçlü bir aşk var. Gençliğin rüzgârıyla hareketlenen İmkânsızın Şarkısını ölümle erken karşılaşan gençlerin hayatı yönlendiriyor. Hiçbir şeyin önem taşımadığı, amaçsızlığın ağır bastığı, özgür seksin kol gezdiği bir öğrenci hayatı... Ama diğer yanda da yoğun duygular var... İmkânsız aşklar, imkânsız şarkılar söyleten. Hemen hemen her Japon gencinin okuduğu roman anayurdu dışında da çok kişi tarafından sahipleniliyor.',
    },
    {
        title: 'Dracula',
        author: 'Bram Stoker',
        pageNum: 200,
        imageURL:
            'https://i.idefix.com/cache/600x600-0/originals/0001828853001-1.jpg',
        topic: 'İrlandalı yazar Bram Stoker’ın, iki taraf arasındaki bu irade ve güç çatışmasını işlediği ve korku edebiyatının başyapıtlarından biri sayılan Dracula, yayımlanmasının üzerinden yüz yılı aşkın süre geçmesine karşın, bugün de aynı ilgiyle okunuyor.',
    },
    {
        title: 'Karamazov Kardeşler',
        author: 'Fyodor Mihayloviç Dostoyevski',
        pageNum: 200,
        imageURL:
            'https://i.idefix.com/cache/600x600-0/originals/0001803800001-1.jpg',
        topic: 'İrlandalı yazar Bram Stoker’ın, iki taraf arasındaki bu irade ve güç çatışmasını işlediği ve korku edebiyatının başyapıtlarından biri sayılan Dracula, yayımlanmasının üzerinden yüz yılı aşkın süre geçmesine karşın, bugün de aynı ilgiyle okunuyor.',
    },
    {
        title: 'Sultanın Korsanları',
        author: 'Emrah Safa Gürcan',
        pageNum: 296,
        imageURL:
            'https://i.idefix.com/cache/600x600-0/originals/0001780787001-1.jpg',
        topic: '1968-1970 yılları arasında geçen olaylar, o günün toplumsal gerçeklerini de satırlara taşıyor. Ama romanın odağında bu toplumsal olaylar değil üçlü bir aşk var. Gençliğin rüzgârıyla hareketlenen İmkânsızın Şarkısını ölümle erken karşılaşan gençlerin hayatı yönlendiriyor. Hiçbir şeyin önem taşımadığı, amaçsızlığın ağır bastığı, özgür seksin kol gezdiği bir öğrenci hayatı... Ama diğer yanda da yoğun duygular var... İmkânsız aşklar, imkânsız şarkılar söyleten. Hemen hemen her Japon gencinin okuduğu roman anayurdu dışında da çok kişi tarafından sahipleniliyor.',
    },
];

for (const { title, author, pageNum } of books) {
    console.log(title + ': ' + author + ': ' + pageNum);
}




//INFO:6 - REST Operator in Object Literals


/* const person = {
    name: 'John',
    surname: 'Doe',
    age: 40,
    fullName() {
        return this.name + " " + this.surname
    }
} */

/* const {name, surname, age} = person;
console.log(name); */

/* const {name, ...rest} = person;
console.log(name);
console.log(rest); */

//INFO:7 - Object Values - Object Entries
console.log("Object Keys:", Object.keys(person));//[..]
console.log("Object Values:",Object.values(person));//[..]
console.log("Object Entries:",Object.entries(person));//[['name','John']]