import pandas as pd
from sklearn.preprocessing import StandardScaler
from sklearn.cluser import KMeans
import joblib


def train_model(input_path="HorrorMovieBackend/ML/user_features.csv", model_path="HorrorMovieBackend/ML/user_cluster_model.pkl"):
