## Simple game logger with recording into file.
### Installation Package Manager

Open Package Manager =>
Tap on plus button => 
Add package from git URL =>
Put inside https://github.com/dragoff/game-logger.git

## Using 
Log as usual,
```c#
GDebug.Log("Message");
GDebug.LogWarning("Warning");
GDebug.LogError("Error");
```
![Image1](Images/GameLogger_pic3.png)

Log with title,  
```c#
GDebug.Log("Message", ("GameLogger", Color.red));
```
![Image2](Images/GameLogger_pic4.png)

Log to file only, 
```c#
GDebug.LogToFile("Message");
``` 
![Image3](Images/GameLogger_pic2.png)

Start logging into empty file and rename the old file to GameLog.old,  
```c#
GDebug.ResetLogFile();
```        

## Saves the logs to a persistent path.

***Note:***  Contains menu item button to open persistent path.

![Image4](Images/GameLogger_pic1.png)
