import User from "@/types/User"
import HttpClient from "@/HttpClient"
import LoginRequest from "@/types/LoginRequest"

export default class UserService {
  public async login(request: LoginRequest): Promise<any> {
    var result = await HttpClient.post('/login', request);
    return result.data;
  }
}
