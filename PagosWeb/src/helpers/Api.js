import axios from 'axios'

export default () => {

    var headers = {
        'Accept': 'application/json',
        'Content-Type': 'application/json',
    }

    const axiosInstance = axios.create({
        baseURL: "http://localhost:5013/api/",
        withCredentials: false,
        headers: headers,
    })

    return axiosInstance;
}