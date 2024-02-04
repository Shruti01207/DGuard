import { Injectable, inject } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { TokenResponse } from '../models/token-response.model';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { StartConversation } from '../models/start-conversation-response.model';
import { SendMessageRequest } from '../models/send-message-request.model';
import { BotResponse } from '../models/getbot-response.model';



@Injectable({
  providedIn: 'root'
})


export class ChatService {

  constructor() { }

  private http = inject(HttpClient);

  getToken(): Observable<TokenResponse> {
    return this.http.get<TokenResponse>(`${environment.apiBaseUrl}/api/Bot/token`);
  }

  getTokenAndStartConversation(): Observable<StartConversation> {
    return this.http.post<StartConversation>(`${environment.apiBaseUrl}/api/Bot/conversation`, '');
  }

  sendMessage(request: SendMessageRequest): Observable<any> {
    return this.http.post<any>(`${environment.apiBaseUrl}/api/Bot/user_message`, request);
  }

  getBotResponse(request: BotResponse):Observable<any>{
    return this.http.post<any>(`${environment.apiBaseUrl}/api/Bot/bot_message`, request);
  }
  
  refreshToken(currentToken: string):Observable<any>{
   
    const headers = { 'Authorization': 'Bearer '+ currentToken }
    return this.http.post<any>('https://directline.botframework.com/v3/directline/tokens/refresh','',{ headers });

  }


}
