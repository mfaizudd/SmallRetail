import Product from '@/types/Product'
import HttpClient from '@/HttpClient'
export default class ProductService {
  public async getProducts (limit = 10, page = 1): Promise<Product> {
    const result = await HttpClient.get(`/products?limit=${limit}&page=${page}`)
    return result.data
  }

  public async getProduct (id: string): Promise<Product> {
    const result = await HttpClient.get(`/products/${id}`)
    return result.data
  }

  public async create (product: Product): Promise<any> {
    const result = await HttpClient.post('/products', product)
    return result.data
  }

  public async update (id: string, product: Product): Promise<any> {
    const result = await HttpClient.put(`/products/${id}`, product)
    return result.data
  }

  public async delete (id: string): Promise<any> {
    const result = await HttpClient.delete(`/products/${id}`)
    return result.data
  }
}
