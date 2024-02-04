import { AfterContentChecked, Component, ElementRef, OnDestroy, OnInit, ViewChild, inject } from '@angular/core';
import { ChatService } from '../services/chat.service';
import { Observable, Subscription, map, mergeMap, switchMap } from 'rxjs';
import { SendMessageRequest } from '../models/send-message-request.model';
import { StartConversation } from '../models/start-conversation-response.model';
import { MessageInfo } from '../models/message.model';
import { interval } from 'rxjs';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';


@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],

 
})
export class HomeComponent implements OnInit, OnDestroy {

  @ViewChild('chatContainer', { static: true }) chatContainer?: ElementRef;

  private token?: string;
  private conversationId?: string;
  public messageRequest: SendMessageRequest;
  private previousResponse: number = -1;
  public sub?: any;

  // messages:string[ ]=["Hi"];
  messages: MessageInfo[] = [
    {
      text: '',
      sender: 'bot',
      type: 'actions',
      actions: [{ title: "What is Diabetes" }, { title: 'What is cause of diabetes' }]

    }
  ];

  constructor() {
    this.messageRequest = {
      Message: '',
      Token: this.token as string,
      ConversationId: this.conversationId as string
    }
  }

  ngOnDestroy(): void {
    this.chatServiceSubcription?.unsubscribe();
  }

  private chatService = inject(ChatService);
 

  private chatServiceSubcription?: Subscription;
  print() {
    console.log("message", this.messageRequest.Message);
  }

  ngOnInit() {
    this.startConversation();
    this.refreshTokenFunc();
  }


  getDirectLineToken() {
    this.chatServiceSubcription = this.chatService.getToken().subscribe({
      next: (response) => {
        console.log("token", response);
      }
    })
  }

  async startConversation(): Promise<StartConversation | undefined> {
    this.chatServiceSubcription = this.chatService.getTokenAndStartConversation().
      subscribe({
        next: async (response) => {
          console.log(response);
          this.messageRequest.Token = response.token;
          this.messageRequest.ConversationId = response.conversationId;
          //  await this.message();
          return response;
        }
      });
    return undefined;
  }

  message() {

    this.messages.push({ text: this.messageRequest.Message, sender: "user", type: "text", actions: [] });

    this.chatServiceSubcription = this.chatService.sendMessage(this.messageRequest).
      subscribe({
        next: (response) => {
          console.log("message posted", response);
          this.messageRequest.Message = "";
          this.botResponse();

          //polling interval
          setTimeout(() => {
            console.log("timeout");
            this.botResponse();
            var messageBody = document.querySelector('#chat-container') as Element;
            console.log(messageBody);
            messageBody.scrollTop = messageBody.scrollHeight - messageBody.clientHeight;
            console.log(messageBody.scrollTop, messageBody.scrollHeight, messageBody.clientHeight);
          }, 4000);
        },
        error: (response) => {
          console.log(response);
        }
      })


  }

  botResponse() {

    this.chatServiceSubcription = this.chatService.getBotResponse({
      token: this.messageRequest.Token,
      conversationId: this.messageRequest.ConversationId
    }).subscribe({
      next: (response) => {
        console.log("bot-response", response);

        // for (let i = (this.previousResponse + 1); i < response?.length; i++) {
        //   this.messages.push({ text: response[i]?.text ? response[i].text : response[i].speak, sender: "bot" });
        // }
        // this.previousResponse = response.length - 1;
        this.getNextAction(response);
      }
    })

  }
  refreshToken() {
    console.log("token", this.messageRequest.Token);
    this.chatService.refreshToken(this.messageRequest.Token as string).subscribe({
      next: (response) => {
        console.log("token refreshed", response);
      }
    })
  }

  refreshTokenFunc() {
    interval(3500).subscribe(x => {
      //this.refreshToken( );
    });
  }



  getNextAction(response: any) {

    for (let i = (this.previousResponse + 1); i < response?.length; i++) {
      //this.messages.push({ text: response[i]?.text ? response[i].text : response[i].speak, sender: "bot" });

      if (response[i].text) {
        this.messages.push({
          text: response[i].text,
          sender: 'bot',
          type: "text",
          actions: []
        });
      }
      else if (response[i].speak) {
        this.messages.push({
          text: response[i].speak,
          sender: 'bot',
          type: "text",
          actions: []
        });
      }

      if (response[i].suggestedActions) {
        this.messages.push({
          text: ' ',
          sender: 'bot',
          type: "actions",
          actions: response[i].suggestedActions.actions
        });
      }

    }
    this.previousResponse = response.length - 1;
    console.log("this.messages", this.messages);

  }


  selectSuggestion(i: number, j: number) {
    this.messageRequest.Message = this.messages[i].actions[j].title;
    this.message();

  }





}
