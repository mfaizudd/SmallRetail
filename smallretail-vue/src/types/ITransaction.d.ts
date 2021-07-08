import ITransactionProduct from "./ITransactionProduct";

export default interface ITransaction
{
  id: string;
  transactionProducts: ITransactionProduct[];
  dateCreated: Date;
  dateUpdated: Date;
}
