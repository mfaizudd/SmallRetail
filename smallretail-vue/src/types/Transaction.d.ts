import TransactionProduct from './TransactionProduct'

export default interface Transaction
{
  id: string;
  transactionProducts: TransactionProduct[];
  dateCreated: Date;
  dateUpdated: Date;
}
