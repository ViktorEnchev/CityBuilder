import { axiosPostUnauth } from "src/axios/axios";
import { useNotification } from "src/effects/useNotification";

export const login = async (data) => {
  try {
    const response = await axiosPostUnauth("identity", data);

    if (response.status === 200) {
      localStorage.setItem("token", `Bearer ${response.data.token}`);
      return true;
    }
    return false;
  } catch (e) {
    return false;
  }
};
