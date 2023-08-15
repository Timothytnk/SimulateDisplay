# SimulateDisplay
This project is targeting to mirror the screen of a program loaded in ram, it replicates that screen and displays it in a picture box or streams frames from that app and 
saves it to a video file.
This is very much achievable by subscribing to the Application.Idle event handler and then you read Bitmaps from one window and mirror them in the other control.
