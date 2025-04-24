# Horror Movie Tracker

**HorrorMovieTracker** (https://github.com/scomitchell/HorrorMovieTracker) is a personalized movie tracking app that allows users to log horror films they've watched, add and read reviews, and discover new movies.

## Features
- User Authentication: Secure login and registration using JWT authentication
- Personalized movie lists: Each user has a private list of movies they've watched that cannot be seen or modified by any other user.
- Add new movies: Registered users can add new movies to the shared catalog and then to their personal list.
- Frontend search bar: Filter and search through movies instantly from the shared catalog, handled entirely on the frontend.
- Shared movie pool: All movies are stored once in the database and referenced across user lists to avoid duplication.
- Reviews: Users can anonymously rate and review movies in the database and read other users' comments.

## Tech Stack
- Frontend: React + CSS
- Backend: ASP.NET Core (C#)
- Database: SQLite
- Auth: JWT + localStorage
