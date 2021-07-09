import IProduct from '@/types/IProduct';
import axios from 'axios';

export default class ProductService
{
  API_URL = process.env.VUE_APP_API_URL

  public async getBooks(limit: number = 10, page: number = 1) : Promise<IProduct>
  {
    let result = await axios.get(`${this.API_URL}/products?limit=${limit}&page=${page}`);
    return result.data;
  }
}
