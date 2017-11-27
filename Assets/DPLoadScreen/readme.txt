------------
DPLoadScreen
Version v1.2
------------

Created by Davi Santos (davisan2@gmail.com)


-----
About
-----

DPLoadScreen acts as a replacement for the Application.LoadLevel which allows to show a custom load screen. It requires Unity 4.X Pro or any version of Unity 5.


----------------------
How to view the sample
----------------------

1) Make sure the scene files "LoadScreen.unity", "StartScene.unity" and "BigScene.unity" are added in the "Scenes in build".
2) Open the scene "StartScene.unity" and run. Click the button and a BigScene will be loaded showing the load screen.


-----------------------
How to use DPLoadScreen
-----------------------

1) Open the scene "/Assets/DPLoadScreen/LoadScreen.unity" and add it to the "Scenes in build" in "File->Build Settings"

2) Customize the scene "LoadScreen.unity" with your images, animations, etc. 

3) The property DPLoadScreen.Instance.Progress has a value between 0-100 that indicates the percentage of the loading.

4) Whenever you want to change the scene, rather than using Application.LoadLevel("BigScene") use DPLoadScreen.Instance.LoadLevel("BigScene").

5) Profit!


-------------------
v1.2 Bug fixes and new feature
-------------------

- Fixed a wrong maximum value of the example scene slider and the docs.
- Addictive Scene support!

-------------------
v1.1 New Features!!
-------------------

- Unity 5.3 compatible
- Now you can activate the scene manually! Useful to create a "Press Any Key to Continue" when the loading is completed!


----------
Questions?
----------
Contact Us: davisan2@gmail.com