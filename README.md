# WPF-Sprint-Board
Simple Sprint Board application that allows the user to track a sprints progress with the time left and time taken for each task in a sprint. I wrote this application to allow me to track sprints after we moved in to new offices where cards on the wall were a thing of the past. We use it with a Smartboard and I have made some adjustments to where it is today. It is a simple WPF applicaiton that uses 4 text files to stores its information I did this for speed at the time but am looking to move to SQLite with Entity Framework soon.

Full features include:
- Multiple sprints with in the same app
- Stores who is working on a task with the use of an avatar
- Multiple users on a task
- Can drag tasks cards from Todo through to complete
- Can track hours left on a task
- Can track the time taken on a task
- Store images against sprints based on naming convention

Features/items I plan to include:
- Move away from text files to SQLite with Entity Framework
- Create sprints within the application
- Link to Trello to bring through stories
