import React from "react";
import { useParams } from "react-router-dom"

function MoviePage() {
    let { id } = useParams();
    return <h1>Movie details for ID: {id}</h1>;
}

export default MoviePage;