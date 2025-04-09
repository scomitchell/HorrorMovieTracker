import React from "react";
import { Routes, Route } from "react-router-dom";
import Navbar from "./components/Navbar";
import Home from "./pages/Home";
import Movies from "./pages/Movies";
import MoviePage from "./pages/MoviePage";
import RecentReleases from "./pages/RecentReleases";
import Login from "./components/Login";
import Registration from "./components/Registration";
import MovieForm from "./components/MovieForm";
import MyMovies from "./pages/MyMovies";
import NotFound from "./pages/NotFound";
import { AuthProvider } from "./context/AuthContext";

function App() {
    return (
        <AuthProvider>
            <Navbar />
            <Routes>
                <Route path="/" element={<Home />} />
                <Route path="/movies" element={<Movies />} />
                <Route path="/movies/:id" element={<MoviePage />} />
                <Route path="/recentreleases" element={<RecentReleases />} />
                <Route path="/login" element={<Login />} />
                <Route path="/register" element={<Registration />} />
                <Route path="/addmovie" element={<MovieForm />} />
                <Route path="/my-movies" element={<MyMovies />} />
                <Route path="*" element={<NotFound />} />
            </Routes>
        </AuthProvider>
    );
}

export default App;

