# Textual Reality Experience Engine

Today we are spoilt for choices when it comes to computer entertainment. We have games consoles like the XBOX and Playstation offering up 4k hyper-real graphics with immersive sound sitting under our TV sets. We also have virtual reality experiences that immerse us entirely in graphics and sound with headsets, augmented reality where we overlay graphics onto the real world, and we have mixed reality with headsets likes the Hololens. These are all well and good, but we can go even higher resolution using the most powerful graphics engine ever invented; your imagination!

With that, I present this game engine called the "Textual Reality Experience Engine." Ever since Humans could communicate in writing, we have been using the power of the written word to tell stories and fire up the imagination. Back in the 1980s making fiction more interactive was popularized with text adventure computer games where you get to play and explore a virtual world purely through text on the screen. At the time computers were very underpowered and could not display lots of graphics, but as graphic based computer games became more popular the humble text adventure started fading into the background, but they never went away, and there are still a lot of people who enjoy playing them as well as making them.

The Textual Reality Experience Engine is an easy to use, object-oriented game framework designed to make developing modern text adventures easier. The engine is developed in C# on the .NET Core platform (supporting .NET Standard 2.* and higher); which means games can be played in the console window, online (if you use Blazor to load the game dll's client side) or on mobile platforms using Xamarin.

Some of the features of Textual Reality include:

- Navigation around rooms that are linked together.
- A flexible parser that breaks text commands down into Verb, noun, preposition, noun groups.
- An extensible system for adding a verb and noun synonyms, so the player doesn't have to hunt to the correct specific word.
- Profanity filtering, so the use of profane words are highlighted by the parser. The game engine doesn't perform any censoring, but profanity detection can make a fun way to react back to the player.
- In build content management system so that text can be abstracted away from the game logic.
- Save / load game system.
