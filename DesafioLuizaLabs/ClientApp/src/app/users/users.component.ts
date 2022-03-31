import { Component, OnInit } from '@angular/core';
import { UserDataService } from '../_data-services/user.data-service';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css']
})
export class UsersComponent implements OnInit {

  users: any[] = [];
  user: any = {};
  userLogin: any = {};
  userChancePassword: any = {};
  userLogged: any = {};
  showList: boolean = true;
  showForgotPassword: boolean = false;
  showChangePassword: boolean = false;
  idAuthenticated: string = "";
  isAuthenticated: boolean = false;

  constructor(private userDataService: UserDataService) { }

  ngOnInit() {
    
  }

  get() {
    this.userDataService.get().subscribe((data:any[]) => {
      this.users = data;
      this.showList = true;
    }, error => {
      console.log(error);
      alert(error.error)
    })
  }

  save() {
    if (this.user.id) {
      this.put();
    } else {
      this.post();
    }    
  }

  openDetails(user) {
    this.showList = false;
    this.user = user;
  }

  post() {
    this.userDataService.post(this.user).subscribe(data => {
      if (data) {
        alert('Usuário cadastrado com sucesso');
        this.get();
        this.user = {};
      } else {
        alert('Erro ao cadastrar usuário');
      }
    }, error => {
      console.log(error);
      alert(error.error)
    })
  }

  put() {
    this.userDataService.put(this.user).subscribe(data => {
      if (data) {
        alert('Usuário atualizado com sucesso');
        this.get();
        this.user = {};
      } else {
        alert('Erro ao atualizar usuário');
      }
    }, error => {
      console.log(error);
      alert(error.error)
    })
  }

  delete() {
    this.userDataService.delete().subscribe(data => {
      if (data) {
        alert('Usuário excluído com sucesso');
        this.get();
        this.user = {};
      } else {
        alert('Erro ao excluir usuário');
      }
    }, error => {
      console.log(error);
      alert(error.error)
    })
  }

  voltar() {
    this.showList = true;
    this.showForgotPassword = false;
  }

  logout() {
    this.isAuthenticated = false;
    this.user.email = "";
    this.user.senha = "";
    this.user.confirmacaosenha = "";
  }

  authenticate() {
    if (this.userLogin.email && this.userLogin.senha) {
      this.userDataService.authenticate(this.userLogin).subscribe((data: any) => {
        if (data.user) {
          localStorage.setItem('user_logged', JSON.stringify(data));
          this.get();
          this.getUserData();
        } else {
          alert('Usuário Inválido.');
        }
      }, error => {
        console.log(error);
        alert(error.error)
      })
    }
    else {
      alert("Informe o E-mail e a Senha!");
    }
  }

  forgot_password() {
    if (this.userLogin.email) {
      this.userDataService.forgot_password(this.userLogin).subscribe((data: any) => {
        if (data) {
          this.get();
          this.showForgotPassword = false;
          this.showChangePassword = true;
          alert("Sua senha foi redefinida e enviada ao seu e-mail.")
        } else {
          alert('Usuário Inválido.');
        }
      }, error => {
        console.log(error);
        alert(error.error)
      })
    }
    else {
      alert("Informe o E-mail!");
    }
  }

  change_password() {
    if (this.userChancePassword.senha && this.userChancePassword.confirmacaosenha) {
      this.userChancePassword.id = this.userLogged.user.id;
      this.userChancePassword.email = this.userLogged.user.email;
      this.userDataService.change_password(this.userChancePassword).subscribe((data: any) => {
        if (data) {
          this.get();
          this.showForgotPassword = false;
          this.showChangePassword = false;
          alert("Sua senha foi redefinida e enviada ao seu e-mail.")
        } else {
          alert('Usuário Inválido.');
        }
      }, error => {
        console.log(error);
        alert(error.error)
      })
    }
    else {
      alert("Informe o a nova Senha e a Confirmação!");
    }
  }

  getUserData() {
    this.userLogged = JSON.parse(localStorage.getItem('user_logged'));
    this.isAuthenticated = this.userLogged != null;
  }

}
