export interface RegisterRequest {
  userName: string;
  email: string;
  password: string;
  firstName: string;
  lastName: string;
  sex: string;
  dateOfBirth: Date;
  phoneNumber: string;
  currentCityID: string;
  description?: string;
  observedCityIDs?: string[];
}
