using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Crypto.Models;
using Crypto.Models.ViewModels;
using Crypto.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace Crypto.Controllers
{
    public class TransactionController : Controller
    {
        public int perPage = 4;
        private ITransactions repository;
        private TransactionsViewModel defaultView;
        private TransactionsViewModel searched;
        private IUser users;
        private IReport reports;
        private IWallet wallets;
        private ILoginUser Loki;
        IEnumerable<String> supportedCurency = new List<String> { "BitCoin", "BitCoinCash", "Dash", "Ethereum", "LightCoin", "Monero", "Ripple", "Zcash", };

        public TransactionController(ITransactions tranny, IUser Users, IReport repot, IWallet wallet, ILoginUser loki)
        {
            users = Users;
            repository = tranny;
            reports = repot;
            wallets = wallet;
            Loki = loki;
            defaultView = new TransactionsViewModel
            {
                Transactions = repository.Transactions,
                SearchForma = new SearchForm
                {
                    CurrencyType = "All",
                    MinAmount = 0,
                    MaxAmount = 10000,
                    Active = true,
                    UserName = "",
                    SupportedWallets = supportedCurency

                },
                PagingInfo = new ViewModels.PagingInfo
                {
                    CurrentPage = 1,
                    ItemsPerPage = perPage,
                    TotalItems = repository.Transactions.Count()
                }

            };
        }

        [HttpGet]
        public ViewResult List1() => View("List", new TransactionsViewModel
        {
            Transactions = repository.Transactions.OrderBy(t => t.TransactionId)
            .Skip((1 - 1) * perPage).Take(perPage),
            PagingInfo = new ViewModels.PagingInfo
            {
                CurrentPage = 1,
                ItemsPerPage = perPage,
                TotalItems = repository.Transactions.Count()
            },
            SearchForma = new SearchForm
            {
                CurrencyType = "All",
                MinAmount = 0,
                MaxAmount = 10000,
                Active = true,
                UserName = "",
                SupportedWallets = supportedCurency

            }
        });

               
    [HttpGet]
        public ViewResult List(int page = 1) => View(new TransactionsViewModel
        {
            Transactions=repository.Transactions.OrderBy(t => t.TransactionId)
                            .Skip((page-1)*perPage).Take(perPage),
            PagingInfo= new ViewModels.PagingInfo
            {
                CurrentPage = page,
                ItemsPerPage = perPage,
                TotalItems= repository.Transactions.Count()
            },
            SearchForma = new SearchForm
            {
                CurrencyType = "All",
                MinAmount = 0,
                MaxAmount = 10000,
                Active = true,
                UserName = "",
                SupportedWallets = supportedCurency

            }

        });
        [HttpPost]
        public ViewResult List(SearchForm search, int page = 1)
        {
            searched = new TransactionsViewModel
            {
                Transactions = repository.Transactions,
                SearchForma = search,
                PagingInfo = defaultView.PagingInfo,

            };
            searched.SearchForma.SupportedWallets = supportedCurency;

            if (search.CurrencyType != "All")
            {
                searched.Transactions = searched.Transactions.Where(p => p.CurrencyType == search.CurrencyType);
            }

            if (search.MinAmount != null && search.MinAmount != null)
            {
                searched.Transactions = searched.Transactions.Where(p => p.Amount >= search.MinAmount && p.Amount <= search.MaxAmount);
            }

            if (search.MinAmount != null && search.MaxAmount == null)
            {
                searched.Transactions = searched.Transactions.Where(p => p.Amount >= search.MinAmount);
            }

            if (search.MinAmount == null && search.MaxAmount != null)
            {
                searched.Transactions = searched.Transactions.Where(p => p.Amount <= search.MaxAmount);
            }


            if (search.UserName != null)
            {
                searched.Transactions = searched.Transactions.Where(p => p.UserName == search.UserName);
            }
           
    //       TempData["stuff"] = persistent;

            searched.PagingInfo.TotalItems = searched.Transactions.Count();
            IEnumerable<Transaction> persistent = searched.Transactions;
            string workaround = "";
            foreach(var p in persistent)
            {
                workaround += p.TransactionId + " ";
            } 
            HttpContext.Session.SetString("Test", workaround);
            searched.Transactions = searched.Transactions.OrderBy(t => t.TransactionId)
                .Skip((page - 1) * perPage).Take(perPage);


            return View("SearchResult", searched);
        }
        [HttpGet]
        public ViewResult SearchResult(int page = 1)
        {
            string code = HttpContext.Session.GetString("Test");
            HttpContext.Session.SetString("Test", code);
            string[] codes = code.Split(" ");
             List<Transaction> coder = new List<Transaction>();
            foreach(string str in codes)
            {
                if(str == null || str == "")
                {
                    break;
                }
                coder.Add(repository.Transactions.Where(p => p.TransactionId.ToString() == str).FirstOrDefault());
            }

            IEnumerable<Transaction> thisSearch = coder; 

            return View("SearchResult", new TransactionsViewModel
            {
                Transactions = thisSearch.OrderBy(t => t.TransactionId)
                            .Skip((page - 1) * perPage).Take(perPage),
                PagingInfo = new ViewModels.PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = perPage,
                    TotalItems = thisSearch.Count()
                },
                SearchForma = new SearchForm
                {
                    CurrencyType = "All",
                    MinAmount = 0,
                    MaxAmount = 10000,
                    Active = true,
                    UserName = "",
                    SupportedWallets = supportedCurency

                }
            });
    }
        [HttpGet]
        [Authorize]
        public ViewResult CreateTransaction() {

            User user = users.Users.Where(w => w.UserName == HttpContext.User.Identity.Name).FirstOrDefault();
            Transaction newTransaction = new Transaction();
            CreateTransactionViewModel CTVM = new CreateTransactionViewModel {
                TransactionWallets = wallets.Wallets.Where(p=> p.UserID == user.AcountID),
                SupportedWallets = supportedCurency};
            if(CTVM.TransactionWallets == null)
            {
                TempData["message"] = "You must import a wallet before you can create a transaction";
                return View("List", 1);
            }
            return View("CreateTransaction",CTVM);
        }
        [HttpPost]
        public IActionResult CreateTransaction(CreateTransactionViewModel ctvm, string information)
        {
            User current = users.Users.Where(l => l.UserName == HttpContext.User.Identity.Name).FirstOrDefault();
                if(ModelState.IsValid)
                {
                    Transaction t = new Transaction();
                    t.Amount = ctvm.Amount;
                    t.AcountId = current.AcountID;
                    t.AmountWanted = ctvm.AmountWanted;
                    t.CurrencyType = ctvm.CurrencyType;
                    t.CurrrencyWanted = ctvm.CurrrencyWanted;
                    t.TransactionDescription = ctvm.TransactionDescription;
                    t.TransactionInfo = information;
                    t.TransactionTitle = ctvm.TransactionTitle;
                    t.UserName = current.UserName;
                    t.Wallet = ctvm.Wallet;
                    repository.SaveTransaction(t);
                    TempData["message"] = $"{t.TransactionTitle} has been created";
                return RedirectToAction("List1");
                }
                else
                {
                CreateTransactionViewModel CTVM = new CreateTransactionViewModel
                {
                    // NewTransaction = ctvm.NewTransaction,
                    TransactionWallets = wallets.Wallets.Where(p => p.UserID == current.AcountID),
                    SupportedWallets = supportedCurency
                        
                    };
                    if (CTVM.TransactionWallets == null)
                    {
                        TempData["message"] = "You must import a wallet before you can create a transaction";
                        return View("List", repository.Transactions);
                    }
                    return View(CTVM);
                }
            //}
            //else
            //{
                TempData["message"] = "Invalid Wallet Selected";
                return View(ctvm);
            //}

        }
        [HttpPost]
        public ViewResult Stuff(int id)
        {
            SpecificTransactionViewModel STVM = new SpecificTransactionViewModel
            {

                Trans = repository.GetTransaction(id),
                CurrentUser = users.GetUser(repository.GetTransaction(id).AcountId)
            };

            return View("SpecificTransaction", STVM);
        }

        public ViewResult Prev(int id, SpecificTransactionViewModel STVM)
        {

            if (id > 1)
            {
                STVM.Trans = repository.GetTransaction(id - 1);
                STVM.CurrentUser = users.GetUser(repository.GetTransaction(id - 1).AcountId);
            }
            else
            { return View("List", defaultView); }
            
            return View("SpecificTransaction",STVM);
        }

        public ActionResult Next(int id, SpecificTransactionViewModel STVM)
        {

            if ((id +1) < repository.Transactions.Last().TransactionId)
            {
                STVM.Trans = repository.GetTransaction(id + 1);
                STVM.CurrentUser = users.GetUser(repository.GetTransaction(id + 1).AcountId);
            }
            else if ((id +1) == repository.Transactions.Last().TransactionId)
            {
                STVM.Trans = repository.Transactions.Last();
                STVM.CurrentUser = users.GetUser(repository.GetTransaction(id + 1).AcountId);
            }
            else
            { return RedirectToAction("List1"); }

            return View("SpecificTransaction", STVM);
        }
        [Authorize]
        [HttpPost]
        public ViewResult Report(int id, int idUser, string reason, string explination)
        {
            User reporter = users.Users.Where(J => J.UserName == HttpContext.User.Identity.Name).FirstOrDefault();
            Report report = new Report();
            report.AcountReported = repository.Transactions.Where(u => id == u.TransactionId).First().AcountId;
            report.AcountReporting = reporter.UserId;
            report.Description = explination;
            report.Reason = reason;
            reports.SaveReport(report);
   
            SpecificTransactionViewModel STVM = new SpecificTransactionViewModel
            {

                Trans = repository.GetTransaction(id),
                CurrentUser = users.GetUser(repository.GetTransaction(id).AcountId)
            };
            return View("SpecificTransaction", STVM);
        }

        [HttpGet]
        [Authorize]
        public ViewResult ImportWallet() => View(new WalletModel {
            Wally= new Wallet(),
            AcceptedCurrency = supportedCurency
        });
        [HttpPost]
        public ActionResult ImportWallet(Wallet newWallet)
        {
            if(ModelState.IsValid)
            {
                newWallet.UserID= users.Users.Where(u => u.UserName == HttpContext.User.Identity.Name).FirstOrDefault().AcountID;
                wallets.SaveWallet(newWallet);
            }
            else
            {
                return RedirectToAction("ImportWallet");
            }
            return RedirectToAction("List1");
        }













        public ViewResult ViewReport() => View(reports.Reports);
        [HttpGet]
        public ViewResult Login()
        {
            return View();
        }

        [HttpPost]
        public ViewResult Login(string userName, string password)
        {
            User temp = users.Users.Where(p => p.UserName == userName && p.Password == password).FirstOrDefault();
            if(temp == null)
            {
                TempData["message"] = "Invalid Login Credentials, Please try again";
                return View("Login");
            }
            else
            {
                LoginUser loki = new LoginUser {
                    UserId = temp.UserId,
                    UserName = temp.UserName};
                Loki.Login(loki);
                TempData["message"] = "Login Successfull";
                return View("List", defaultView);
            }
        }
        public ViewResult Logout()
        {
            Loki.LogOut(Loki.Logs.LastOrDefault());
            TempData["message"] = "Logout Successfull";
            return View("List", defaultView);
        }

       /* private IEnumerable<Transaction> GetSearch()
        {
           IEnumerable<Transaction> search = HttpContext.Session.GetJson<IEnumerable<Transaction>>("SearchResult");
            return search;
        }*/
    }
}
