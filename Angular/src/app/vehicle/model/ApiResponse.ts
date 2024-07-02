export class ApiResponse<T> {
    Success: boolean;
    message?: string;
    data?: T;
  }
  