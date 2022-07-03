'use strict';

const modal = document.querySelector('.modal'); 
const overlay = document.querySelector('.overlay')
const btnCloseModal = document.querySelector('.close-modal'); 
const btnsShowModal = document.querySelectorAll('.show-modal');//query selector brings first of multiple, therefore use All


const closeModal =  function(){
    overlay.classList.add('hidden');
    modal.classList.add('hidden');
}

const openModal =  function(){
    overlay.classList.remove('hidden');
    modal.classList.remove('hidden');
}

//for any button with show-modal class.
for (let i = 0; i < btnsShowModal.length; i++) {
    btnsShowModal[i].addEventListener('click', openModal); //DRY
}


//DRY : dont repeat yourself.
btnCloseModal.addEventListener('click',closeModal);
overlay.addEventListener('click', closeModal);


//document: everywhere of the page.
//we need event to see what key was pressed.
document.addEventListener('keydown', function(event){
    if(event.key === 'Escape' &&  !modal.classList.contains('hidden'))
        closeModal();
});


