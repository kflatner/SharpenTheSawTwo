import axios from 'axios';

export const http = axios.create({
  baseURL: process.env.VUE_APP_API_BASEURL,  // will resolve depending on environment
  withCredentials: false,
});
