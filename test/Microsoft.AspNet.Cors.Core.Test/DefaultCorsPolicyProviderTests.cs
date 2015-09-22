// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Threading.Tasks;
using Microsoft.AspNet.Http.Internal;
using Xunit;

namespace Microsoft.AspNet.Cors.Core.Test
{
    public class DefaultPolicyProviderTests
    {
        [Fact]
        public async Task UsesTheDefaultPolicyName()
        {
            // Arrange
            var options = new CorsOptions();
            var policy = new CorsPolicy();
            options.AddPolicy(options.DefaultPolicyName, policy);

            var corsOptions = new TestCorsOptions
            {
                Value = options
            };
            var policyProvider = new DefaultCorsPolicyProvider(corsOptions);

            // Act 
            var actualPolicy = await policyProvider.GetPolicyAsync(new DefaultHttpContext(), policyName: null);

            // Assert
            Assert.Same(policy, actualPolicy);
        }

        [Theory]
        [InlineData("")]
        [InlineData("policyName")]
        public async Task GetsNamedPolicy(string policyName)
        {
            // Arrange
            var options = new CorsOptions();
            var policy = new CorsPolicy();
            options.AddPolicy(policyName, policy);

            var corsOptions = new TestCorsOptions
            {
                Value = options
            };
            var policyProvider = new DefaultCorsPolicyProvider(corsOptions);

            // Act 
            var actualPolicy = await policyProvider.GetPolicyAsync(new DefaultHttpContext(), policyName);

            // Assert
            Assert.Same(policy, actualPolicy);
        }
    }
}