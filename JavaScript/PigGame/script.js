'use strict';

const scoreZero = document.querySelector('#score--0');
const scoreOne = document.getElementById('score--1');
const diceEl = document.querySelector('.dice');
const currentZero = document.getElementById('current--0');
const currentOne = document.getElementById('current--1');
scoreOne.textContent = 0; //js automatically return them into strings to display on the dom.
scoreZero.textContent = 0;
diceEl.classList.add('hidden');


const rollDiceButton = document.querySelector('.btn--roll');
const btnNew = document.querySelector('.btn--new');
const btnHold = document.querySelector('.btn--hold');


const player = document.querySelector('.player'); //default active player.
const playerOne = document.querySelector('.player--1');
const playerZero = document.querySelector('.player--0');


const scores = [0, 0];
let activePlayer = 0;
let currentScore = 0;
let playing = true;

let playerOneScore = document.querySelector(`#score--1`);
let playerZeroScore = document.querySelector(`#score--0`);
let playerZeroCurrentScore = document.querySelector('#current--0');
let playerOneCurrentScore = document.querySelector('#current--1');

const createRandomDice = function () {
    return Math.trunc(Math.random() * 6) + 1 //return from 0 to 6.
}

const changeDiceDisplay = function (value) {
    diceEl.src = `dice-${value}.png`;
}

const switchPlayer = () => {
    document.getElementById(`current--${activePlayer}`).textContent = 0;
    currentScore = 0;
    activePlayer = activePlayer == 0 ? 1 : 0;
    playerOne.classList.toggle('player--active'); // if playerOne's classList contains player--active, delete it, if not add it.
    playerZero.classList.toggle('player--active'); //same.

}

const resetGame =  function(){
  
    playing = true;
    currentScore = 0;
    activePlayer = 0;
    scores[0] = 0;
    scores[1] = 0;
    
    playerOneScore.textContent = 0;
    playerZeroScore.textContent = 0;
    playerOneCurrentScore.textContent =0;
    playerZeroCurrentScore.textContent =0;


    document
    .querySelector('.player--winner')
    .classList.remove('player--winner');
    
    document
    .querySelector(`.player--${activePlayer}`)
    .classList.add('player--active');

    
    diceEl.classList.remove('hidden');
    
}


//rolling dice functionality.
rollDiceButton.addEventListener('click', function () {

    if (playing) {
        //create random dice
        const num = createRandomDice();
        //display dice.
        diceEl.classList.remove('hidden');
        changeDiceDisplay(num);

        //check for rolled 1, if it's switch to next player.
        if (num !== 1) {
            //add dice to the current score.
            currentScore += num;
            //show the current score;
            document.getElementById(`current--${activePlayer}`).textContent = currentScore;
        } else {
            switchPlayer();
        }
    }

})



btnHold.addEventListener('click', function () {
    if (playing) {
        scores[activePlayer] += currentScore;
        document.getElementById(`score--${activePlayer}`).textContent = scores[activePlayer];

        if (scores[activePlayer] >= 20) {
            playing = false;
            diceEl.classList.add('hidden');
            document
                .querySelector(`.player--${activePlayer}`)
                .classList.add('player--winner');

            document.querySelector(`.player--${activePlayer}`)
                .classList.remove('player--active');

        } else {
            switchPlayer();
        }
    }

})


btnNew.addEventListener('click', resetGame)

//1. first check out who's the active player
//2. check out if active player's score is more than 100 or not.
//3. if it's more than 100, player wins, else switch player.

