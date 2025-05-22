import React, { useEffect, useState } from "react"
import { useParams } from "react-router-dom"
import ReviewForm from "../components/ReviewForm"
import "../styles/MoviePage.css"
import "../styles/Universal.css"

// Displays specific movie details
function MoviePage() {
    let { id } = useParams();
    const [movie, setMovie] = useState(null);
    const [reviews, setReviews] = useState([]);
    const [averageRating, setAverageRating] = useState(0);

    useEffect(() => {
        fetch(`http://localhost:5004/api/movies/${id}`)
            .then((response) => response.json())
            .then((data) => setMovie(data))
            .catch((error) => console.error("Error fetching movie:", error));
    }, [id]);

    useEffect(() => {
        fetch(`http://localhost:5004/api/reviews/movie/${id}`)
            .then((response) => response.json())
            .then((data) => setReviews(data))
            .catch((error) => console.error("Error fetching reviews:", error));
    }, [id]);

    useEffect(() => {
        fetch(`http://localhost:5004/api/reviews/movie/${id}/rating`)
            .then((response) => response.json())
            .then((data) => setAverageRating(data))
            .catch((error) => console.error("Error fetching average rating:", error));
    }, [id]);

    if (!movie) {
        return <h2>Loading...</h2>
    }

    return (
        <div class="individual-movies">
            <h2 className="page-header-movie"> {movie.title} ({new Date(movie.releaseDate).toLocaleDateString()}) </h2>
            <hr className="page-divider-nrml" />
            <h3 className="movie-subgenre">Subgenre: {movie.subgenre}</h3>
            {averageRating !== null && (
                <h4 className="mb-3">Average Rating: {averageRating.toFixed(1)} / 5</h4>
            )}
            <div class="description-imageurl">
                {movie.imageUrl && <img src={movie.imageUrl} alt={`${movie.title} Poster`}/>}
                <p className="description-box">{movie.description}</p>
            </div>
            <div className="reviews-section">
                <h3>Reviews</h3>
                {reviews.length > 0 ? (
                    reviews.map((review) => (
                        <div key={review.id} className="review">
                            <h3>Anonymous User Review:</h3>
                            <p><strong>Rating:</strong> {review.rating}</p>
                            <p>{review.content}</p>
                        </div>
                    ))
                ) : (
                    <p>No reviews yet.</p>
                )}
            </div>
            <ReviewForm movieId={movie.id}/>
        </div>
    );
}

export default MoviePage;