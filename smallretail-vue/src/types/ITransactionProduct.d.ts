import IProduct from './IProduct'

export default interface ITransactionProduct
{
  transactionId: string;
  productId: string;
  product: IProduct;
  quantity: number;
}
