import React, { useEffect, useState } from "react"
import { useParams } from "react-router-dom"
import ReviewForm from "../components/ReviewForm"
import "../styles/MoviePage.css"

function MoviePage() {
    let { id } = useParams();
    const [movie, setMovie] = useState(null);

    useEffect(() => {
        fetch(`http://localhost:5004/api/movies/${id}`)
            .then((response) => response.json())
            .then((data) => setMovie(data))
            .catch((error) => console.error("Error fetching movie:", error));
    }, [id]);

    if (!movie) {
        return <h2>Loading...</h2>
    }

    return (
        <div class="individual-movies">
            <h1> {movie.title} ({new Date(movie.releaseDate).toLocaleDateString()}) </h1>
            <h2>Subgenre: {movie.subgenre}</h2>
            <div class="description-imageurl">
                {movie.imageUrl && <img src={movie.imageUrl} alt={`${movie.title} Poster`}
                    style={{ width: "200px", borderRadius: "8px" }} />}
                <p>{movie.description}</p>
            </div>
            <ReviewForm movieId={movie.id}/>
        </div>
    );
}

export default MoviePage;