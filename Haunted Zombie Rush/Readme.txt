Created with C# and Unity Engine

Game: Haunted Zombie Rush
Design inspiration: Flappy Birds

Game Mechanics:
    - Player must avoid endless obstacles
    - Energy system
        - if player runs out of energy, they die
        - player must eat coins to regain lost energy

Patch 1.00
    Purpose:
        - Create initial playable game

Patch 1.01 
    Goals:
        - adjust size ratios to not feel clunky 
            and better balance player jump decisions
        - add sound effect to coins
        - make coins feel more attractive
        - make ui more natural.
        - basic instructions?
        - make game have a point?

    Changelog:
        - Adjusted UI positioning to clear up clutter
            - elements clamped to bot of screen
            - rearranged menu positioning
        - Player size ajusted 40% smaller
            - note: this fixed size ratios
        - Coins
            - new mesh, texture and material
            - new partical system added
            - added sound effect when consumed
            - coins now give energy back to player
        - Level environment
            - added a visible moving ceiling
        - Main menu
            - Added exit application button
            - Added instruction button
        - New energy resource
            - Added resource "Energy"
            - Energy now continuously drains
            - When energy is gone, player dies

    Notes:
        - Game has a much better overall purpose to it with 
            the addition of the energy system. Players must 
            not only avoid obstacles, but must also keep getting
            coins or they will die. 
        - The coins are redesigned. 
        - Ceiling creates makes the level feel less forced. 
    
    Further Improvement Options:
        - Add hit splatters to create better emphasis on collider hits
        - Redesign background to move. 