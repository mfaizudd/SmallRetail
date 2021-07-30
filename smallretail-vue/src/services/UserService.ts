import User from "@/types/User"
import HttpClient from "@/HttpClient"
import LoginRequest from "@/types/LoginRequest"

export default class UserService {
  public async login(request: LoginRequest): Promise<any> {
    var result = await HttpClient.post('/login', request);
    return result.data;
  }

  public async getUsers(limit = 10, page = 1): Promise<any> {
    var result = await HttpClient.get(`/users?limit=${limit}&page=${page}`)
    return result.data
  }

  public async getUser(id: string): Promise<User> {
    var result = await HttpClient.get(`/users?id=${id}`);
    return result.data
  }
}
