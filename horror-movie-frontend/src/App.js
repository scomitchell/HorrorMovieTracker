import logo from './logo.svg';
import './App.css';
import Home from './pages/Home';
import Movies from './pages/Movies';
import MoviePage from './pages/MoviePage'
import NotFound from './pages/NotFound'
import Navbar from './components/Navbar'
import RecentReleases from "./pages/RecentReleases"
import { Routes, Route } from "react-router-dom";

function App() {
    return (
        <div>
            <Navbar />
            <Routes>
                <Route path="/" element={<Home />} />
                <Route path="/movies" element={<Movies />} />
                <Route path="/movies/:id" element={<MoviePage />} />
                <Route path="/recentreleases" element={<RecentReleases/>} />
                <Route path="*" element={<NotFound />} />
            </Routes>
        </div>
    );
}

export default App;
