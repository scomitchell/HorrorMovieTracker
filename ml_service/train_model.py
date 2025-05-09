import pandas as pd
from sklearn.preprocessing import StandardScaler
from sklearn.cluster import KMeans
import joblib


def train_model(input_path="HorrorMovieBackend/ML/user_features.csv",
                model_path="HorrorMovieBackend/ML/user_cluster_model.pkl"):

    df = pd.read_csv(input_path, index_col=0)

    # Preprocessing
    df_encoded = pd.get_dummies(df, columns=["TopSubgenre"], drop_first=True)
    x = df_encoded.dropna()

    scaler = StandardScaler()
    x_scaled = scaler.fit_transform(x)

    model = KMeans(n_clusters=4, random_state=7)
    model.fit(x_scaled)

    joblib.dump((model, scaler, df_encoded.columns.tolist()), model_path)
    print(f"Model trained and saved to {model_path}")


if __name__ == "__main__":
    train_model()


