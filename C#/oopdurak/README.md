  **OOP 4200 Durak**

## Playing The Game

The main menu has four buttons.

1. The Play button - Starts the game.
2. The Options button - Allows you to choose different textures for the cards.
3. The Stats button - Displays user stats and provides way to reset stats and player name.
4. The Exit button - Exit the game.

## The Options Screen

![Options Screen](/C%23/oopdurak/docs/options.PNG)

1. You can select the size of the deck prior to playing a game.
   A small deck contains 20 cards (10, J, Q, K, A).
   A standard deck contains 36 cards (6 - A).
   A large deck contains the full 52 cards within a deck.

2. You can preview the textures for cards and even select a different set of
   textures.

3. The help button opens this document in the default web browser.

4. The main menu button returns you to the main menu.

## The Stats Screen

![Stats Screen](/C%23/oopdurak/docs/stats.PNG)

This screen displays your user name at the top as well as a summary of your
performance. To change your name and reset your stats, you must click on the
reset stats button.

## The Game Screen

![Game Screen](/C%23/oopdurak/docs/game.PNG)

1. This image represents the Talon.
2. This is the trump card for the game.
3. The AI Player's hand.
4. The center area is the _playing field_ where any played cards will show up.
   The cards are placed from left to right, starting with the attacking card,
   then the defending card.
5. The area on the bottom is the player's hand as well as the concede button.
   The concede button will be active after the first attack or during a defense,
   clicking it will pass the player's turn.

## Card Skins

Durak provides the ability to customize the textures for the cards in the game.  
The skin packs are located in the `assets/textures/cardskins` folder within the
game directory. Each skin pack is a sub-folder within the aforementioned directory
that is named after the skin pack.

> For example, a set of card skins called `Big Numbers` will have a its folder
> named `Big_Numbers` (spaces are replaced by underscores) and have a directory
> structure as such:
>
> `<path-to-game-dir>/assets/textures/cardskins/Big_Numbers/<texture-files>`

The textures themselves are either PNGs or JPEGs. One texture exists for each
card plus an additional texture for the backside of all cards. The textures are
named as follows:

`<lower-case-suit>_<rank>.(png | jpg)`

For example:

+ `spade_ace.png`
+ `diamond_8.jpg`
+ `heart_king.jpg`
+ `club_6.png`

### Creating Custom Card Skins

Creating custom skin packs is simple. Simply copy one of the existing skin packs
renaming the folder to the name of your new skin pack. Then, open up the folder
and edit the textures within in your preferred image editor. When you launch
Durak you will be able to choose your newly created skin pack in the `options`
menu.
