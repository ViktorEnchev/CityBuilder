import axios from "axios";
import { baseUrl } from "src/common/constants";

export const axiosGet = (url) =>
  axios.get(`${baseUrl}${url}`, {
    headers: { Authorization: localStorage.getItem("token") },
  });

export const axiosPost = (url, data) =>
  axios.post(`${baseUrl}${url}`, data, {
    headers: { Authorization: localStorage.getItem("token") },
  });

export const axiosPostUnauth = (url, data) =>
  axios.post(`${baseUrl}${url}`, data);

export const axiosDelete = (url) =>
  axios.delete(`${baseUrl}${url}`, {
    headers: { Authorization: localStorage.getItem("token") },
  });
