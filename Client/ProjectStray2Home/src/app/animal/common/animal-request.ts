export interface AnimalRequest {
  id?: string;
  type: string;
  breed: string;
  name: string;
  sex: string;
  dateOfBirth?: Date;
  characteristic: string;
  behavior: string;
  isChipped: boolean;
  status: string;
  statusDescription?: string;
  cityID: string;
  userName: string;
}
