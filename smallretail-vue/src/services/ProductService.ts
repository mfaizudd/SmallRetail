import Product from '@/types/Product';
import axios from 'axios';

export default class ProductService
{
  API_URL = process.env.VUE_APP_API_URL

  public async getProducts(limit: number = 10, page: number = 1): Promise<Product>
  {
    let result = await axios.get(`${this.API_URL}/products?limit=${limit}&page=${page}`);
    return result.data;
  }

  public async getProduct(id: string): Promise<Product>
  {
    let result = await axios.get(`${this.API_URL}/products/${id}`);
    return result.data;
  }

  public async create(product: Product): Promise<any>
  {
    let result = await axios.post(`${this.API_URL}/products`, product);
    return result.data;
  }

  public async update(id: string, product: Product): Promise<any>
  {
    let result = await axios.put(`${this.API_URL}/products/${id}`, product);
    return result.data;
  }

  public async delete(id: string): Promise<any>
  {
    let result = await axios.delete(`${this.API_URL}/products/${id}`);
    return result.data;
  }
}
