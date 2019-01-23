using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Text;
using Microsoft.AspNetCore.WebUtilities;
using OGOship.Model;
using RestSharp;
using RestSharp.Authenticators;

namespace OGOship
{
    public class OAuthSession
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public int expires_in { get; set; }
        public string refresh_token { get; set; }
    }

    public class OgoShipApi
    {
        public string TokenType { get; set; }
        public string AccessToken { get; set; }
        public DateTime ExpiresIn { get; set; }
        public string RefreshToken { get; set; }

        public string ClientId { get; set; }
        public string ClientSecred { get; set; }
        public string Scope { get; set; }

        public string AuthServer = "https://my.ogoship.com";
        public string ApiServer = "https://api.ogoship.com";

        public OgoShipApi()
        {

        }

        public bool Authenticate(string username, string password, string clientId, string clientSecret, string scope)
        {
            var authClient = new RestClient($"{AuthServer}/OAuth/Token");
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("grant_type", "password");
            request.AddParameter("username", username);
            request.AddParameter("password", password);
            request.AddParameter("client_id", clientId);
            request.AddParameter("client_secret", clientSecret);
            request.AddParameter("scope", scope);
            var response = authClient.Execute<OAuthSession>(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                TokenType = response.Data.token_type;
                AccessToken = response.Data.access_token;
                ExpiresIn = DateTime.Now.AddSeconds(response.Data.expires_in);
                RefreshToken = response.Data.refresh_token;

                ClientId = clientId;
                ClientSecred = clientSecret;
                Scope = scope;

                return true;
            }

            return false;
        }

        public bool RefreshTokenNow()
        {
            var authClient = new RestClient($"{AuthServer}/OAuth/Token");
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("grant_type", "refresh_token");
            request.AddParameter("refresh_token", RefreshToken);
            request.AddParameter("client_id", ClientId);
            request.AddParameter("client_secret", ClientSecred);

            var response = authClient.Execute<OAuthSession>(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                TokenType = response.Data.token_type;
                AccessToken = response.Data.access_token;
                ExpiresIn = DateTime.Now.AddSeconds(response.Data.expires_in);
                RefreshToken = response.Data.refresh_token;
                Debug.WriteLine($"RefreshTokenNow success.");

                return true;
            }

            Debug.WriteLine($"RefreshTokenNow failed.");

            return false;

        }

        public IRestResponse<T> Execute<T>(RestRequest request) where T : new()
        {
            if (ExpiresIn < DateTime.Now)
                RefreshTokenNow();

            var client = new RestClient(ApiServer);
            if (AccessToken != null)
                client.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(AccessToken, "Bearer");


            var result = client.Execute<T>(request);

            return result;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modifiedAfter"></param>
        /// <param name="reference"></param>
        /// <returns></returns>
        public List<OrderResponse> GetOrders(DateTime? modifiedAfter = null, string reference = null)
        {
            var fullList = new List<OrderResponse>();

            var limit = 50;
            var page = 0;
            var singleResult = limit;
            while (singleResult == limit)
            {
                page++;
                Debug.WriteLine($"Request Orders page {page}");

                var request = new RestRequest("/api/v1/orders", Method.GET);

                request.AddQueryParameter("limit", limit.ToString());
                request.AddQueryParameter("page", page.ToString());
                if (modifiedAfter.HasValue)
                    request.AddQueryParameter("modifiedAfter", modifiedAfter.Value.ToString("O"));
                if (!string.IsNullOrWhiteSpace(reference))
                    request.AddQueryParameter("reference", reference);

                var apiResponse = Execute<List<OrderResponse>>(request);
                if (apiResponse.IsSuccessful)
                {
                    singleResult = apiResponse.Data.Count;
                    fullList.AddRange(apiResponse.Data);
                }
                else
                {
                    throw new Exception("Error");
                }
            }

            return fullList;
        }


        /// <summary>
        /// Add new order
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public OrderResponse AddOrder(Order order)
        {
            Debug.WriteLine($"Add new order");

            var request = new RestRequest($"/api/v1/orders", Method.POST);
            request.AddJsonBody(order);

            var apiResponse = Execute<OrderResponse>(request);

            if (!apiResponse.IsSuccessful)
            {
                throw new Exception("Error");
            }

            return apiResponse.Data;
        }

        /// <summary>
        /// Get products
        /// </summary>
        /// <param name="modifiedAfter"></param>
        /// <param name="code">Get products by code (SKU)</param>
        /// <param name="eanCode">Get products by ean</param>
        /// <returns></returns>
        public List<ProductResponse> GetProducts(DateTime? modifiedAfter = null, string code = null, string eanCode = null)
        {
            var fullList = new List<ProductResponse>();

            var limit = 50;
            var page = 0;
            var singleResult = limit;
            while (singleResult == limit)
            {
                page++;
                Debug.WriteLine($"Request Products page {page}");

                var request = new RestRequest("/api/v1/products", Method.GET);

                request.AddQueryParameter("limit", limit.ToString());
                request.AddQueryParameter("page", page.ToString());
                if (modifiedAfter.HasValue)
                    request.AddQueryParameter("modifiedAfter", modifiedAfter.Value.ToString("O"));
                if (!string.IsNullOrWhiteSpace(code))
                    request.AddQueryParameter("code", code);
                if (!string.IsNullOrWhiteSpace(eanCode))
                    request.AddQueryParameter("eanCode", eanCode);

                var apiResponse = Execute<List<ProductResponse>>(request);
                if (apiResponse.IsSuccessful)
                {
                    singleResult = apiResponse.Data.Count;
                    fullList.AddRange(apiResponse.Data);
                }
                else
                {
                    throw new Exception("Error");
                }
            }

            return fullList;
        }

        /// <summary>
        /// Add new product
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public ProductResponse AddProduct(Product product)
        {
            Debug.WriteLine($"Request product update");

            var request = new RestRequest($"/api/v1/products", Method.POST);
            request.AddJsonBody(product);

            var apiResponse = Execute<ProductResponse>(request);

            if (!apiResponse.IsSuccessful)
            {
                throw new Exception("Error");
            }

            return apiResponse.Data;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="product"></param>
        /// <param name="oldCode"></param>
        /// <returns></returns>
        public ProductResponse UpdateProduct(Product product, string oldCode = null)
        {
            Debug.WriteLine($"Request product update");

            if (string.IsNullOrWhiteSpace(oldCode))
                oldCode = product.Code;

            var encodedString = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(oldCode));

            var request = new RestRequest($"/api/v1/products/{encodedString}", Method.PUT);
            request.AddQueryParameter("base64url", "true");
            request.AddJsonBody(product);

            var apiResponse = Execute<ProductResponse>(request);

            if (!apiResponse.IsSuccessful)
            {
                throw new Exception("Error");
            }

            return apiResponse.Data;
        }

        /// <summary>
        /// Get stock levels
        /// </summary>
        /// <param name="modifiedAfter"></param>
        /// <param name="productCode"></param>
        /// <returns></returns>
        public List<StockResponse> GetStockLevels(DateTime? modifiedAfter = null, string productCode = null)
        {
            var fullList = new List<StockResponse>();

            var limit = 50;
            var page = 0;
            var singleResult = limit;
            while (singleResult == limit)
            {
                page++;
                Debug.WriteLine($"Request Stock page {page}");

                var request = new RestRequest("/api/v1/stock", Method.GET);

                request.AddQueryParameter("limit", limit.ToString());
                request.AddQueryParameter("page", page.ToString());
                if (modifiedAfter.HasValue)
                    request.AddQueryParameter("modifiedAfter", modifiedAfter.Value.ToString("O"));
                if (!string.IsNullOrWhiteSpace(productCode))
                    request.AddQueryParameter("productCode", productCode);

                var apiResponse = Execute<List<StockResponse>>(request);
                if (apiResponse.IsSuccessful)
                {
                    singleResult = apiResponse.Data.Count;
                    fullList.AddRange(apiResponse.Data);
                }
                else
                {
                    throw new Exception("Error");
                }
            }

            return fullList;
        }
    }
}
