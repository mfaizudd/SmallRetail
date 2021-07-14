import Product from './Product'

export default interface TransactionProduct
{
  transactionId: string;
  productId: string;
  product: Product;
  quantity: number;
}
