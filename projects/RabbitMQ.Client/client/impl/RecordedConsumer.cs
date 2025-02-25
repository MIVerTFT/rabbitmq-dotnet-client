// This source code is dual-licensed under the Apache License, version
// 2.0, and the Mozilla Public License, version 2.0.
//
// The APL v2.0:
//
//---------------------------------------------------------------------------
//   Copyright (c) 2007-2020 VMware, Inc.
//
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
//
//       https://www.apache.org/licenses/LICENSE-2.0
//
//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License.
//---------------------------------------------------------------------------
//
// The MPL v2.0:
//
//---------------------------------------------------------------------------
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
//
//  Copyright (c) 2007-2020 VMware, Inc.  All rights reserved.
//---------------------------------------------------------------------------

using System.Collections.Generic;

namespace RabbitMQ.Client.Impl;

#nullable enable
internal readonly struct RecordedConsumer
{
    public AutorecoveringModel Channel { get; }
    public IBasicConsumer Consumer { get; }
    public string Queue { get; }
    public bool AutoAck { get; }
    public string ConsumerTag { get; }
    public bool Exclusive { get; }
    public IDictionary<string, object>? Arguments { get; }

    public RecordedConsumer(AutorecoveringModel channel, IBasicConsumer consumer, string queue, bool autoAck, string consumerTag, bool exclusive, IDictionary<string, object>? arguments)
    {
        Channel = channel;
        Consumer = consumer;
        Queue = queue;
        AutoAck = autoAck;
        ConsumerTag = consumerTag;
        Exclusive = exclusive;
        Arguments = arguments;
    }

    public static RecordedConsumer WithNewConsumerTag(string newTag, in RecordedConsumer old)
    {
        return new RecordedConsumer(old.Channel, old.Consumer, old.Queue, old.AutoAck, newTag, old.Exclusive, old.Arguments);
    }

    public static RecordedConsumer WithNewQueueNameTag(string newQueueName, in RecordedConsumer old)
    {
        return new RecordedConsumer(old.Channel, old.Consumer, newQueueName, old.AutoAck, old.ConsumerTag, old.Exclusive, old.Arguments);
    }

    public string Recover(IModel channel)
    {
        return channel.BasicConsume(Queue, AutoAck, ConsumerTag, false, Exclusive, Arguments, Consumer);
    }
}
