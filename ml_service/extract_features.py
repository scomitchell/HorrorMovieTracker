import sqlite3
import pandas as pd


def extract_user_features(db_path="HorrorMovieBackend/HorrorMovie.db",
                          output_path="HorrorMovieBackend/ML/user_features.csv"):
    conn = sqlite3.connect(db_path)

    # Load relevant tables
    user_movies = pd.read_sql_query("SELECT * FROM UserMovies", conn)
    movies = pd.read_sql_query("SELECT * FROM Movies", conn)
    reviews = pd.read_sql_query("SELECT * FROM Reviews", conn)

    # Merge to get movie info per user
    merged = user_movies.merge(movies, left_on="MovieId", right_on="Id")

    # Feature - Number of Movies watched
    movie_counts = user_movies.groupby("UserId").size().rename("Watched Count")

    # Feature - Average Movie Release Year
    avg_year = merged.groupby("UserId")["ReleaseDate"].apply(
        lambda x: pd.to_datetime(x).dt.year.mean()
    ).rename("AvgReleaseYear")

    # Feature - Most Common Subgenre
    top_subgenre = merged.groupby("UserId")["Subgenre"].agg(
        lambda x: x.mode().iloc[0] if not x.mode().empty else "None")
    top_subgenre.name = "TopSubgenre"

    # Feature - Number of reviews written
    num_reviews = reviews.groupby("UserId").size().rename("ReviewCount")

    # Merge all features
    features = pd.concat([movie_counts, avg_year, top_subgenre, num_reviews], axis=1).fillna(0)
    features.to_csv(output_path)

    print(f"Exported user features to {output_path}")


if __name__ == "__main__":
    extract_user_features()




